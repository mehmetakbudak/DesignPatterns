/*
 Single design pattern bir nesnenin application pool kapanana kadar bir kez üretilmesini ve tek bir instance'ının olmasını kontrol altında tutar. Aynı zamanda bu nesne sınıf dışından da erişilebilir olur. Bir sınıfın bir anda sadece bir örneğinin olması istenildiği zamanlarda kullanılır. Örneğin veritabanı uygulamalarında bir anda bir bağlantı nesnesinin olması sistem kaynaklarının verimli bir şekilde kullanılmasını sağlar. Bu nesnenin kullanımı oldukça basittir. Singleton deseni uygulanacak sınıfın constructor metodu private olarak tanımlanır ve sınıfın içinde kendi türünden static bir sınıf tanımlanır. Tanımlanan bu sınıfa erişim sağlayacak bir metot veya property de sınıfa eklenir. Bu desenin birden fazla kullanım şekli olsa da genel anlamda bu şekilde kullanılır.

 */

using System;

Singleton singleton = Singleton.GetInstance();
Console.WriteLine(singleton.Id.ToString());


Singleton singleton2 = Singleton.GetInstance();
Console.WriteLine(singleton2.Id.ToString());


Console.ReadKey();

class Singleton
{
    private static Singleton _instance;
    private static Guid _id; // nesnenin tek olduğunun ispatı

    public Guid Id { get { return _id; } }

    // singleton sınıfına ait sınıfın çalışma zamanında constructor dan yararlanarak oluşturulmamasını sağlar.
    private Singleton() { }

    public static Singleton GetInstance()
    {
        if (_instance == null)
            _instance = new Singleton();

        _id = Guid.NewGuid();


        return _instance;
    }
}
