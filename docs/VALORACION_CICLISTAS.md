### Sistema de valoración basado en potencia absoluta

---

#### Resúmen
**Idea principal:** usar **potencia absoluta (W)** como la métrica primaria normalizada a **0–100** y mostrar **W/kg** como información secundaria. Mantener la misma unidad y lógica de normalización para todas las duraciones para que el sistema sea intuitivo y consistente para el usuario.

---

#### Principio y ventajas
- **Consistencia:** el usuario siempre ve la misma unidad (W) como base de la valoración.  
- **Claridad:** la cifra principal es un **score 0–100** derivado de potencia absoluta; la relación potencia/peso se muestra entre paréntesis para dar contexto.  
- **Simplicidad:** una normalización lineal es fácil de explicar y entender; opcionalmente se puede usar una curva no lineal para ajustar jugabilidad.  
- **Ejemplo de uso:** 20 min con \(P_{min}=150\) W y \(P_{max}=520\) W.

---

#### Fórmula de normalización lineal
Para una duración dada define **\(P_{min}\)** y **\(P_{max}\)**. El **score absoluto 0–100** se calcula así:

\[
\textbf{Score} = \frac{P - P_{min}}{P_{max} - P_{min}} \cdot 100
\]

- **Clamp**: si \(P < P_{min}\) entonces Score = 0; si \(P > P_{max}\) entonces Score = 100.  
- **W por kilo** se calcula como:

\[
W/kg = \frac{P}{masa}
\]

Si quieres un **score relativo 0–100** basado en \(W/kg\) define \(W/kg_{min}\) y \(W/kg_{max}\) y aplica la misma fórmula de normalización.

---

#### Ejemplos calculados
| **Perfil** | **Potencia 20 min** | **Masa** | **Score absoluto** | **W/kg** | **Score relativo** |
|---|---:|---:|---:|---:|---:|
| Contrarrelojista | 500 W | 80 kg | 95 | 6.25 | 75 |
| Escalador | 420 W | 60 kg | 73 | 7.00 | 90 |

**Notas sobre la tabla:** los valores usan \(P_{min}=150\) W y \(P_{max}=520\) W para 20 min y \(W/kg_{min}=2.5\), \(W/kg_{max}=7.5\). Los scores están redondeados al entero.

---