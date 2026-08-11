# Flujo de Simulación — Arquitectura DDD + Motor de Juego

Este documento describe el flujo interno de la simulación de carrera ciclista, siguiendo una arquitectura basada en *ticks*, separación estricta de responsabilidades y agregados desacoplados.

El objetivo es garantizar:

- estabilidad de la simulación  
- ausencia de loops infinitos  
- testabilidad  
- claridad conceptual  
- escalabilidad del modelo  

---

## Bucle principal de simulación

La capa de aplicación orquesta el ciclo.  
El aggregate `Simulation` mantiene estado efímero.  
Los servicios de dominio calculan lógica compleja.  
El aggregate `Stage` aporta información persistible (pendientes, terreno…).  
El `SimulationContext` une ambos mundos.

```csharp
while (!simulation.IsFinished)
{
    // 0. Consumir eventos del tick anterior
    var events = simulation.ConsumeEvents();

    // 1. Avanzar un tick (estado efímero)
    simulation.AdvanceTick(context);

    // 2. Actualizar actitudes según eventos y contexto
    attitudeService.UpdateCyclistsAttitudes(simulation, context, events);
}
```

---

## Paso 0 — Consumir eventos almacenados

Antes de avanzar el tick, se recuperan los eventos generados en el tick anterior:

- ataques  
- contraataques  
- cortes  
- reagrupamientos  
- aceleraciones  
- cambios tácticos  

Estos eventos **no salen del dominio**:  
son efímeros y solo sirven para alimentar el cálculo del tick actual.

```csharp
var events = simulation.ConsumeEvents();
```

---

## Paso 1 — Avanzar un tick (aggregate `Simulation`)

El aggregate `Simulation` actualiza **solo estado efímero**:

### 1.1 Velocidades de cada grupo  
La velocidad depende de:

- actitud de los ciclistas del grupo  
- pendiente actual  
- calidad de carretera  
- viento  
- tipo de terreno  
- energía restante  

El cálculo lo realiza un servicio de dominio (`SpeedCalculator`),  
pero el aggregate aplica los resultados.

---

### 1.2 Distribución de grupos  
Se evalúa:

- separación de grupos (si un ciclista pierde rueda)  
- mergeo de grupos (si un grupo alcanza a otro)  
- reordenación de posiciones  

Tras una separación, se recalculan velocidades para cada nuevo grupo.
La separación y mergeo de grupos generará eventos que se almacenarán en el aggragate root de simulación para su consumo en el próximo tick.

---

### 1.3 Distancia recorrida

Cada grupo avanza según su velocidad:

```csharp
group.Position += speed * TickDuration;
```

---

### 1.4 Estado de los ciclistas

Se actualiza:

- energía  
- desgaste  

---

## Paso 2 — Actualizar actitudes (servicio de dominio)

Con los eventos del tick y el contexto actual (pendiente, terreno, km restante, situación de carrera…), se recalcula la actitud de cada ciclista:

- atacar  
- contraatacar  
- tirar del grupo  
- conservar energía  
- perseguir  
- mantener posición  

Este cálculo **no lo hace el aggregate**, sino un servicio de dominio:

```csharp
attitudeService.UpdateCyclistsAttitudes(simulation, context, events);
```

El aggregate solo almacena el resultado.
Un cambio de actitud crítico (caída, ataque, contraataque...) generará un evento que se almacenará en el aggragate root de simulación para su consumo en el próximo tick.

## Principios clave

- **El aggregate Simulation no conoce la etapa.**  
  Solo conoce posiciones, energía, grupos y eventos.

- **El aggregate Stage no conoce la simulación.**  
  Solo conoce perfil, pendientes, terreno y distancia total.

- **El SimulationContext une ambos.**

- **Los servicios de dominio calculan lógica compleja.**

- **La capa de aplicación orquesta el ciclo.**

- **Los eventos de simulación son efímeros y no salen del dominio.**

---