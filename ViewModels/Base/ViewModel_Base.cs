/************************************
 * БАЗОВЫЙ КЛАСС МОДЕЛИ ПРЕДСТАВЛЕНИЯ
 ***********************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace MyBudget.ViewModels.Base
{
    internal abstract class ViewModel_Base : INotifyPropertyChanged
    {
        /*реализация интерфейса INotifyPropertyChanged*/
        public event PropertyChangedEventHandler PropertyChanged;

        /**********************************************************
         * метод OnPropertyChanged принимает имя свойства и генерирует внутри событие*
         * [CallerMemberName] -> атрибут для компилятора*
         * *********************************************************/
        protected virtual void OnPropertyChanged([CallerMemberName] string PropetyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropetyName));
        }

        /**********************************************************
         * метод Set обновляет значение свойства, для которого определено поле,
         * в котором это свойство хранит данные
         * 'ref T field' -> ссылка на поле свойства
         * 'T value' -> новое значение, которое нужно установить
         * '[CallerMemberName] string PropetyName = null' -> имя свойства (определяется компилятором самостоятельно)
         * '[CallerMemberName] string PropetyName = null' -> передается в метод 'OnPropertyChanged'
         * Метод разрешает кольцевые изменения свойств:
         * когда изменяется свойство 1 система изменяет свойство 2 и т.д.
         **********************************************************/
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropetyName = null)
        {
            if (Equals(field, value)) return false; // если значение уже соответствует тому, что передали -> возвращаем ложь
            field = value; //если значение изменилось -> обнавляем свойство
            OnPropertyChanged(PropetyName);
            return true;
        }
    }
}
