# 🛡️ Felipe Clean Architecture - Estrutura e Decisões de Projeto

Este projeto segue os princípios da **Clean Architecture**, **DDD (Domain-Driven Design)** e **SOLID**, com o uso de **CQRS com MediatR**, **FluentValidation** e separação clara entre camadas.

Abaixo estão documentadas algumas das decisões de projeto mais relevantes, especialmente em relação à separação entre objetos de Request (API), Command (Application), DTOs e Responses.

---

## 📊 Camadas do Projeto

- `Domain` → Entidades de negócio e eventos de domínio
- `Application` → Lógica de negócio, casos de uso, DTOs e Commands/Queries
- `Infrastructure` → Persistência de dados, serviços externos
- `Api` → Controllers, contratos de entrada (Request), resposta (Response), validações com FluentValidation

---

## 📩 Separando `Request` (API) de `Command` (Application)

Mesmo que `CreateTruckRequest` e `CreateTruckCommand` compartilhem os mesmos dados em determinados momentos, optamos por **mantê-los como objetos distintos**, cada um com uma responsabilidade específica.

### ✨ Benefícios da separação

| Aspecto         | Request (API Layer)             | Command (Application Layer)        |
|----------------|----------------------------------|------------------------------------|
| Responsabilidade | Define o contrato HTTP da API  | Executa lógica de negócio          |
| Camada          | Apresentação (HTTP)            | Application Core                   |
| Validação       | FluentValidation (UX, formato) | Regras de negócio                  |
| Evolução        | Pode ter campos extras          | Focado no uso do sistema           |
| Reusabilidade   | Swagger, versionamento          | Handlers, testes, workflows        |

### ✅ Quando manter separados faz sentido?

#### 1. **Campo exclusivo da API**

Exemplo:

```json
{
  "licensePlate": "ABC-1234",
  "model": "Volvo FH",
  "lastMaintenanceDate": "2024-11-10",
  "confirmTerms": true
}
```

de

```csharp
public record CreateTruckRequest(
    string LicensePlate,
    string Model,
    DateTime? LastMaintenanceDate,
    bool ConfirmTerms // <- campo exclusivo da API
);
```


- `confirmTerms` é validado apenas pela API (ex: aceitar termos de uso)
- Não tem utilidade na Application Layer, então não está presente no `CreateTruckCommand`


```csharp
public record CreateTruckCommand(
    string LicensePlate,
    string Model,
    DateTime? LastMaintenanceDate
) : IRequest<Result<TruckOperationDto>>;
```

#### 2. **Validações específicas de interface**

```csharp
RuleFor(x => x.LastMaintenanceDate)
    .LessThanOrEqualTo(DateTime.UtcNow)
    .WithMessage("A data de manutenção não pode estar no futuro.");
```

#### 3. **Evolução desacoplada entre API e Application**
- API pública pode mudar sem impactar comandos internos

#### 4. **Versionamento da API**
- Contratos públicos precisam de estabilidade, enquanto a Application pode evoluir mais rapidamente

### ❌ Quando *pode* usar o Command diretamente no endpoint?
- APIs internas
- Sem regras de validação exclusivas da API
- Estrutura simples e com baixo risco de evolução futura

```csharp
[HttpPost]
public async Task<IActionResult> RegisterTruck(
    [FromBody] CreateTruckCommand command,
    [FromServices] IMediator mediator)
{
    var result = await mediator.Send(command);
    return Ok(result);
}
```

> ⚡️ Use conscientemente. Isso acopla sua API à estrutura interna da aplicação.

---

## 📃 Separando DTOs da Application dos Responses da API

### Problema comum:
Em muitos casos, os dados retornados pela aplicação já estão no formato final. Então: por que ter um `TruckDto` **e** um `TruckResponse`?

### Justificativa:
**Mesmo que os campos sejam iguais hoje**, as responsabilidades são diferentes:

|                       | DTO (Application)               | Response (API)               |
|-----------------------|----------------------------------|------------------------------|
| Camada                | Application                     | API (HTTP)                   |
| Objetivo              | Transferir dados internos        | Apresentar dados ao consumidor |
| Pode conter           | Tipos puros (DateTime, bool)     | Dados formatados (string, texto) |
| Exemplo               | `IsMaintenanceOverdue()` retorna `bool` | Exibe “OK” ou “Vencida” |

### Exemplo aplicado no projeto:
A entidade `Truck` possui:
- `LastMaintenanceDate`
- `RegisteredAt`
- Método `IsMaintenanceOverdue()`

O handler retorna um `TruckDto` assim:

```
new TruckDto(
    t.LicensePlate,
    t.Model,
    t.RegisteredAt,
    t.IsMaintenanceOverdue()
)
```

A API transforma isso em `TruckResponse`:

```
new TruckResponse(
    dto.LicensePlate,
    dto.Model,
    dto.RegisteredAt.ToString("dd/MM/yyyy"),
    dto.MaintenanceOverdue ? "Vencida" : "OK"
)
```

### Dispatching de eventos
No projeto atual, o dispatch de eventos acontece através do `DispatchDomainEventsInterceptor` que foi injetado com `ISaveChangesInterceptor` no `InfrastructureModule`. Contudo, uma das formas de executar manualmente o dispatch seria através do código abaixo em um dos command handlers:

```
        await dispatcher.Dispatch(truck.DomainEvents);
        truck.ClearDomainEvents();
```

### Benefícios:
- A Application permanece agnóstica de formatação ou exibição
- A API apresenta os dados de forma amigável ao consumidor final
- As camadas evoluem com independência

---

## 📆 Conclusão
- Evite usar Commands como entrada da API ou DTOs como saída direta.
- Prefira Requests e Responses específicos na API, e mantenha Commands/Queries/DTOs na Application.

Isso facilita testes, melhora a separação de responsabilidades e reduz impactos em mudanças futuras.

---

## 📚 Referências
- Clean Architecture (Robert C. Martin)
- CQRS with MediatR
- FluentValidation
- ASP.NET Core Best Practices