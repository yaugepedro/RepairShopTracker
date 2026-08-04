namespace RepairShopTracker.Tests.Utils
{
    public static class TestConfig
    {
        // Ponlo en true cuando vayas a grabar el video demostrativo.
        // Ponlo en false para correr las pruebas rápido en el día a día.
        public static bool SlowMotionMode = true;

        // Pausa entre acciones (escribir, hacer clic, navegar)
        public static int StepDelayMs => SlowMotionMode ? 1200 : 0;

        // Pausa al final de cada prueba, antes de cerrar el navegador
        public static int EndOfTestDelayMs => SlowMotionMode ? 2000 : 0;
    }
}