using System;
using System.ComponentModel;

namespace testIOCcontainer
{
    public interface IRegisterableObject
    {
    }

    public interface IRegisterableObject2
    {
        
    }

    public class Student : IRegisterableObject
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(Address address)
        {
            Name = address.Content;
        }
    }

    public class Address
    {
        public string Content { get; set; } = "Shit";
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = MyContainer.GetInstance();

            container.RegisterSingleton<IRegisterableObject,Student>();

            var result = container.GetResult(typeof(IRegisterableObject));

            var result2 = container.GetResult(typeof(IRegisterableObject));
            
            Console.WriteLine(result == result2);


            Console.WriteLine(result.GetType());
        }
    }
}