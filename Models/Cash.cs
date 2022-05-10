/**************************************************/
/*     Модель таблицы Kassa из таблицы БД     */
/**************************************************/

using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Services;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Models 
{
    class Cash
    {
        //Id строки в таблице
        public int Id_String { get; set; }

        // Сумма в общем бюджете
        public decimal CashBalance { get; set; }
    }
}
