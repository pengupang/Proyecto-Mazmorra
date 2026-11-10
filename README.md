# Proyecto Mazmorra ⚔️

Juego 3D de acción en tercera persona desarrollado en **Unity**. El jugador recorre una mazmorra esquivando lava y plataformas móviles, recolecta monedas y se enfrenta a enemigos que lo detectan y lo persiguen.

## Mecánicas

- **Movimiento en tercera persona** con cámara controlada por el mouse, carrera (Shift) y salto.
- **Combate** cuerpo a cuerpo con tiempo de recarga entre ataques.
- **Enemigos con IA simple:** detectan al jugador dentro de un radio, lo persiguen, evitan paredes mediante raycasts y atacan al estar cerca.
- **Sistema de vida** con barra en pantalla, daño y curación.
- **Obstáculos:** suelo de lava mortal y plataformas en movimiento.
- **Recolección de monedas** con contador en el HUD.
- **Pantalla de Game Over**, animaciones de personajes y gestor de audio.

## Tecnologías

- Unity 2022.3 LTS
- C#
- Física 3D de Unity (Rigidbody, colliders, raycasts)
- Animator, TextMesh Pro y Unity UI

## Ejecutar el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/pengupang/Proyecto-Mazmorra.git
   ```
2. Ábrelo desde **Unity Hub** con la versión **2022.3.44f1** (o una 2022.3 LTS compatible).
3. Abre la escena `Assets/Scenes/SampleScene.unity` y presiona **Play**.

### Controles

| Acción | Tecla |
| --- | --- |
| Moverse | W A S D / flechas |
| Correr | Shift izquierdo |
| Saltar | Espacio |
| Atacar | Clic izquierdo |
| Cámara | Mouse |

## Estructura de scripts

| Script | Función |
| --- | --- |
| `PlayerMovement.cs` | Movimiento, cámara, salto y ataque del jugador |
| `Enemigo.cs` | Detección, persecución y ataque de enemigos |
| `PlayerDamage.cs` / `PlayerHealing.cs` / `barraVida.cs` | Vida, daño, curación y barra de vida |
| `Monedas.cs` / `Coins.cs` | Recolección y conteo de monedas |
| `SueloMortal.cs` / `PlatformMovement.cs` | Lava y plataformas móviles |
| `AudioManager.cs` / `gameoverScreen.cs` | Sonido y pantalla de fin de juego |

## Autora

**Maciel Martinez** — GitHub (https://github.com/pengupang) 
**Christofer Rivera** - GitHub (https://github.com/srdango1)
