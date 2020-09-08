using task2.Interfaces;

namespace task2.Interfaces
{
    interface IBaseControl
    {
        virtual void Add() { }
        virtual void Delete(int id) { }
        virtual void Edit(int id) { }
    }
}
