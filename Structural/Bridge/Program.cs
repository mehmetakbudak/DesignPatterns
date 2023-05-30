/*
    Bridge tasarım deseni implementasyonları abstract lardan ayırmak için kullanılır. Örneğin 2 implementasyon sınıfı, bu sınıfkarın türetildiği interface ve bu interface'in bağlı abstract class ı mevcut olsun.
    
    Implementasyon class'ları => Esas fonksiyonaliteyi içerisinde barındıran class'lar
    
    Bridge => Implementasyon class'larının  türediği interface. Bu interface'in görevi abstraction ile implementasyon class'ları arasında köprü görevi görmesi ve onları bağlamasıdır.
    
    Abstraction => Abstract class'ı bridge üzerinden esas class'lara ve onların metotlarına ulaşarak bunları client'a ulaştırır. 

    --- ne zaman kullanmalıyım? ---
    - Implementasyonları client'tan tamamen ayırmak istiyorsanız.
    - Implementasyonları direkt olarak client'la iletişime geçen abstraction'a  bağlamak istemiyorsanız.
    - Abstract class'ını rebuild dahi etmeden implementasyonlar içerisinde değişiklik yapmak istiyorsanız.

    Aşağıda örnekte Abstraction class'ı, main class tarafından ulaşılabilen class'tır. Yani temsili olarak bizim client'ımızın ulaştığı ve fonksiyonel class'ları client'tan soyutlayan class'tır. Bridge ise abstraction ile iletişimde olan class'tır. Bridge class'ı köprü vazifesi gördüğünden tüm fonksiyonel class'ların atası olmak zorundadır.
 */

using System;

Console.WriteLine(new Abstraction(new ImplementationA()).Operation());
Console.WriteLine(new Abstraction(new ImplementationB()).Operation());


public interface IBridge
{
    string OperationImp();
}

class ImplementationA : IBridge
{
    public string OperationImp()
    {
        return "Implementation A";
    }
}

class ImplementationB : IBridge
{
    public string OperationImp()
    {
        return "Implementation B";
    }
}

class Abstraction
{
    IBridge _bridge;

    public Abstraction(IBridge bridge)
    {
        _bridge = bridge;
    }

    public string Operation()
    {
        return $"Abstraction <> {_bridge.OperationImp()}";
    }
}
