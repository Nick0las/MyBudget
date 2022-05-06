/********************************************************************/
/* Модель таблицы из запроса SELECT provider JOIN services_provider */
/********************************************************************/

using System;

namespace MyBudget.Models
{
    class ProviderJoinServices
    {
        //public int IdProvider { get; set; }
        public string NameProvider { get; set; }
        public string ServicesProvider { get; set; }
    }
}
