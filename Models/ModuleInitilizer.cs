using System.Runtime.CompilerServices;

namespace backend.Models {
    public static class MyModuleInitializer {
        [ModuleInitializer]
        public static void Initialize() {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
