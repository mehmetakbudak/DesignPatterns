/*
    Visitor design pattern sınıflara, sınıfların içerisinde değişiklik yapmadan fonksiyonellik ekleme imkanı sunar. Peki ismiyle yaptığın işin benzerliği nedir? Evinize bir ziyaretçi yani visitor geldiğini düşünün. Bazı işlerde size yardım ediyor ve gidiyor. Aslında sizin evin bir üyesi değil. Ama gelip bir iş yapıyor oturuyor kalkıp gidiyor. Visitor design pattern'i de aslında aynı şekilde iş yapar. Visitor class'ın içerisindeki fonksiyonellik aslında, esas ana class'a ait bir fonksiyonellik değildir. Siz visitor class'ı ana class'a inject edersiniz. Inject edili olduğu sürece visitor class kattığı fonksiyonelliği gerçekleştirir. Gün gelip onun yerine başka bir fonksiyonellik için başka bir visitor inject ettiğinizde eski visitor'ın kattığı fonksiyonellik gider ve yerine yeni visitor'un kattığı fonksiyonellik gelir.
 */

using System;


// Aşağıda Car class'ından iki nesne ve Bike class'ından iki nesne oluşturulmuştur. Bu nesneler visit metodları üzerinden ilk olarak PriceVisitor inject edilip çalıştırılmıştır. Arkasından da WeightVisitor. Görüldüğü gibi iki fonksiyonellik de class'ların kendi fonksiyonelliği olmadığı halde bir visitor class üzerinden bir misafişr gibi class'lara inject'e edilip fonksiyonelliklerini çalıştırmıştır. Tekrar bu fonksiyonelliklerin çalıştırılması istenirse tekrar visitor'ların injecte edilmesi gerekecektir.

IStore car1 = new Car { CarName = "A1", Price = 200M, CarType = "Mercedes" };
IStore car2 = new Car { CarName = "A2", Price = 100M, CarType = "Normal" };


IStore bike1 = new Bike { BikeName = "B1", Price = 50M, BikeType = "Bullet" };
IStore bike2 = new Bike { BikeName = "B2", Price = 30M, BikeType = "Normal" };

PriceVisitor priceVisitor = new PriceVisitor();
car1.Visit(priceVisitor);
car2.Visit(priceVisitor);

bike1.Visit(priceVisitor);
bike2.Visit(priceVisitor);

WeightVisitor weightVisitor = new WeightVisitor();
car1.Visit(weightVisitor);
car2.Visit(weightVisitor);

bike1.Visit(weightVisitor);
bike2.Visit(weightVisitor);

Console.ReadKey();

public interface IStore
{
    void Visit(IVisitor visitor);
}

// IStore'u implemente eden Car class'ımız Visit metodu üzerinden inject edilen visitor nesnesi ile o anki visitorun kattığı fonksiyonelliği çalıştırabilecek durumdadır.
public class Car : IStore
{
    public string CarName { get; set; }
    public decimal Price { get; set; }
    public string CarType { get; set; }

    public void Visit(IVisitor visitor)
    {
        visitor.Accept(this);
    }
}

// IStore'u implemente eden Bike class'ımız Visit metodu üzerinden inject edilen visitor nesnesi ile o anki visitorun kattığı fonksiyonelliği çalıştırabilecek durumdadır.
public class Bike : IStore
{
    public string BikeName { get; set; }
    public decimal Price { get; set; }
    public string BikeType { get; set; }

    public void Visit(IVisitor visitor)
    {
        visitor.Accept(this);
    }
}

// PriceVisitor class'ı visitor class'ıdır. İçerisinde Car ve Bike class'ları için fiyat hesaplama fonksiyonelliği bulundurur. Bu fonksiyonellikler Car ve Bike class'ları için çalışıyor olsa da bu class'ların bizzat kendi fonksiyonellikleri olmayıp visitor üzerinden çalıştırılacaktır.
public class PriceVisitor : IVisitor
{
    private const int CAR_DISCOUNT = 5;
    private const int BIKE_DISCOUNT = 2;


    public void Accept(Car car)
    {
        decimal carPriceAfterDiscount = car.Price - ((car.Price / 100) * CAR_DISCOUNT);
        Console.WriteLine($"Car {car.CarName} price : {carPriceAfterDiscount}");

    }

    public void Accept(Bike bike)
    {
        decimal bikePriceAfterDiscount = bike.Price - ((bike.Price / 100) * BIKE_DISCOUNT);
        Console.WriteLine($"Bike {bike.BikeName} price : {bikePriceAfterDiscount}");
    }
}

// WeightVisitor class'ı visitor class'ıdır. İçerisinde Car ve Bike class'ları için ağırlık hesaplama fonksiyonelliği bulundurur. Bu fonksiyonellikler Car ve Bike class'ları için çalışıyor olsa da bu class'ların bizzat kendi fonksiyonellikleri olmayıp visitor üzerinden çalıştırılacaktır.
public class WeightVisitor : IVisitor
{
    public void Accept(Car car)
    {
        switch (car.CarType)
        {
            case "Mercedes":
                Console.WriteLine($"Car {car.CarName} : 1750 kg");
                break;
            case "Normal":
                Console.WriteLine($"Car {car.CarName} : 750 kg");
                break;
        }
    }

    public void Accept(Bike bike)
    {
        switch (bike.BikeType)
        {
            case "Bullet":
                Console.WriteLine($"Bike {bike.BikeName} : 300 kg");
                break;
            case "Normal":
                Console.WriteLine($"Bike {bike.BikeName} : 100 kg");
                break;
        }
    }
}


// IVisitor interface'i her concrete class'ı için inject edildiği yerde kulllanılmak üzere Accept metodu bulundurmakta.
public interface IVisitor
{
    void Accept(Car car);
    void Accept(Bike bike);
}