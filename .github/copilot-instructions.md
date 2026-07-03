# Instrucciones para Copilot

## Objetivo
Este repositorio usa mensajes de commit en español con formato **Conventional Commits**.

## Reglas para generar mensajes de commit
- Redactar siempre en **español**.
- Usar el formato:

  `<tipo>(<ámbito>): <descripción>`

- La descripción debe estar en:
  - tiempo imperativo,
  - minúsculas,
  - sin punto final,
  - breve y clara.
- Si aplica, incluir un ámbito técnico concreto del proyecto.
- No mezclar varios cambios no relacionados en un solo mensaje.
- Si el cambio rompe compatibilidad, marcarlo con:
  - `!` en el encabezado, o
  - `BREAKING CHANGE:` en el cuerpo.

## Tipos permitidos
- `feat`: nueva funcionalidad
- `fix`: corrección de un error
- `docs`: cambios en documentación
- `style`: formato, espacios, sangrías, sin cambios funcionales
- `refactor`: refactorización sin cambio funcional
- `test`: adición o ajuste de pruebas
- `chore`: tareas internas o mantenimiento
- `build`: cambios de compilación o dependencias
- `ci`: cambios de integración continua
- `perf`: mejora de rendimiento
- `revert`: reversión de un commit anterior

## Formato recomendado
- Preferir mensajes concretos.
- Usar un ámbito cuando ayude a identificar el área afectada.
- Si no hay ámbito claro, omitirlo.

## Ejemplos
- `feat(clientes): agregar validación de correo`
- `fix(auth): corregir inicio de sesión con token expirado`
- `docs: actualizar instrucciones de despliegue`
- `refactor(services): simplificar lógica de cálculo`
- `test(clientes): agregar pruebas para alta de cliente`
- `chore: actualizar dependencias del proyecto`

## Instrucción para Copilot
Cuando se solicite un mensaje de commit, proponer una versión en español que siga estas reglas y que describa con precisión el cambio realizado.