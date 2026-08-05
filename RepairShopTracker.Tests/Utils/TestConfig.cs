namespace RepairShopTracker.Tests.Utils
{
    public static class TestConfig
    {
        // Se controla con la variable de entorno SLOW_MOTION.
        // - No la definas (o ponla en "false") para correr las pruebas rápido en el día a día.
        // - Ponla en "true" solo cuando vayas a grabar el video demostrativo:
        //
        //   PowerShell:  $env:SLOW_MOTION="true"; dotnet test
        //   CMD:         set SLOW_MOTION=true && dotnet test
        //
        // Si la variable no existe, por defecto corre en modo rápido (false).
        public static bool SlowMotionMode =>
            Environment.GetEnvironmentVariable("SLOW_MOTION")?.ToLower() == "true";

        // Pausa entre acciones (escribir, hacer clic, navegar)
        public static int StepDelayMs => SlowMotionMode ? 700 : 0;

        // Pausa al final de cada prueba, antes de cerrar el navegador
        public static int EndOfTestDelayMs => SlowMotionMode ? 500 : 0;
    }
}