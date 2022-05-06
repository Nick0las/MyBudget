/**************************************************/
/* Модель таблицы services_provider из таблицы БД */
/**************************************************/

using System;

namespace MyBudget.Models
{
    internal class ProviderServices
    {
        public int IdProviderServices { get; set; }
        public int IdProvider { get; set; }
        public string NameProviderServices { get; set; }
    }
}
