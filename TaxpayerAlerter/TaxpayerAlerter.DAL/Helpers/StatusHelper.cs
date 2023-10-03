using TaxpayerAlerter.DAL.Enums;

namespace TaxpayerAlerter.DAL.Helpers
{
    public static class StatusHelper
    {
        public static string GetStatus(Status status) => status switch
        {
            Status.None => "Не присвоен",
            Status.Passed => "Выполнено",
            Status.ManualCheck => "Ручная проверка",
            Status.Error => "Ошибка",
            _ => "Не присвоен",
        };
    }
}
