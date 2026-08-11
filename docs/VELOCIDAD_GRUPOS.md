# Cálculo de velocidad de un grupo

## Resumen y supuestos
**Resumen:** documento que describe la lógica para determinar la velocidad de un grupo de ciclistas basada en actitudes y niveles de esfuerzo, la formación y comportamiento del **stack de relevos**, y el tratamiento del **escenario de ataque**.  
**Supuestos clave:**  
- **Siempre** hay al menos un ciclista en el grupo.  
- La actitud relevante para relevos es **relevar**.  
- Existe una **velocidad mínima** hardcodeada por nivel de esfuerzo que se usa cuando no hay relevadores o en escenarios concretos.  
- El **stack de relevos** es una estructura FIFO que controla quién tira y cómo rota.  
- En la **creación inicial** del stack (no hay nadie tirando o se ha roto porque viene otro ciclista con mayor intensidad), el primer puesto lo ocupa el ciclista con mayor velocidad teórica.

---

## Algoritmo paso a paso para calcular la velocidad del grupo

1. **Recolectar ciclistas del grupo**  
   - Obtener la lista completa de ciclistas que pertenecen al grupo y sus atributos relevantes: actitud, nivel de esfuerzo, velocidad teórica de relevado, identificador, etc.

2. **Detectar escenario de ataque**  
   - Si existe un ciclista con actitud de **atacar** (o una señal explícita de ataque), el grupo irá a la **velocidad del atacante**.  
   - En este caso se omiten las reglas de stack de relevos y la velocidad mínima; el atacante fija la velocidad del grupo hasta que el ataque cese o se neutralice.

3. **Filtrar por actitud de relevar**  
   - Construir la lista `relevadores = ciclistas.filter(c => c.actitud == Relevar)`.  
   - Si `relevadores` está vacío, la velocidad del grupo = **velocidad mínima** según el nivel de esfuerzo del grupo; terminar.

4. **Calcular velocidades teóricas**  
   - Para cada ciclista en `relevadores` calcular su **velocidad teórica de relevado** `v_teorica` según su nivel de esfuerzo y condiciones.

5. **Comprobar ruptura del grupo**
   - Si el ciclista que mayor velocidad generaría está un 10% por encima de la velocidad actual, el stack de `relevadores` se romperá para crear uno nuevo.

6. **Crear o actualizar el stack de relevos**  
   - **Si se tiene que crear un nuevo stack**: el primer puesto será el ciclista con **mayor `v_teorica`** entre los relevadores; el resto se ordena por criterio de velocidad.  
   - **Si ya hay un tirador**: la inclusión en el stack se decide respecto a la **velocidad actual del grupo** (no respecto a la `v_max` global).  
     - Definir `v_actual` = velocidad que actualmente marca el grupo.  
     - Umbral de entrada = `0.95 * v_actual`.  
     - Incluir en el stack todo ciclista con `v_teorica >= 0.95 * v_actual`.  
   - La entrada se evalúa cada tick o cuando cambian condiciones relevantes.

7. **Establecer la velocidad del grupo**  
   - Si el stack tiene un tirador (primer elemento), la velocidad del grupo = `v_teorica` de ese tirador.  
   - Si el stack queda vacío, aplicar la **velocidad mínima**.

8. **Rotación y duración de la tirada**  
   - Cuando el tirador decide dejar de tirar, se mueve al **final** del stack y su nivel de esfuerzo se establece a **0** durante el siguiente tick.  
   - El siguiente del frente pasa a tirar y marca la velocidad del grupo.

9. **Salida silenciosa**  
   - Si un ciclista dentro del stack baja su `v_teorica` por debajo del umbral de entrada (`0.95 * v_actual`), se **elimina silenciosamente** del stack sin provocar ruptura.  
   - Si la eliminación deja el stack vacío, aplicar la velocidad mínima.

---

#### Ejemplo numérico y pseudocódigo

**Ejemplo 1 Creación inicial**  
- Relevadores y `v_teorica`: A=50 km/h, B=48 km/h, C=40 km/h.  
- No hay tirador (`v_actual` indefinida). Primer puesto = A (50). Stack inicial = [A, B]. Velocidad del grupo = 50 km/h.

**Ejemplo 2 Umbral respecto a velocidad actual**  
- Supongamos `v_actual = 48 km/h`. Umbral = 0.95 * 48 = 45.6 km/h.  
- Relevadores con `v_teorica` 50, 48, 40 → entran 50 y 48; 40 queda fuera. Stack = [tirador, ...] y velocidad = `v_teorica` del tirador.

**Ejemplo 3 Ruptura del grupo**  
- `v_teorica` a 45 km/h y relevadores: A=50 km/h, B=46 km/h, C=45 km/h.  
- Si aparece un relevador D con `v_teorica = 52 km/h`, el grupo pasa a 52 km/h y el stack se reevalúa con la nueva velocidad.
- Con un umbral de 49.4 km/h, el stack pasaría a estar compuesto por el nuevo relevador y A.