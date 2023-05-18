/*
 Farklı şekillede nesnelerin oluşturulmasında, client'ın sadece nesne tipini belirterek creation işlemini gerçekleştirebilmesini sağlamak için kullanılır. Builder design patternda client ın kullanmak istediği bir ürünün birden fazla şekli olabileceği düşünülür. Farklı şekillerin olduğu nesnenin üretiminden builder pattern sorumludur. Dolayısıyla client bu işten soyutlanır. Builder design pattern yer yer factory pattern yer yer de strategy pattern le karıştırılmaya müsaittir. Bu sebeple builder tasarım kalıbını ayırt edebilmemiz için odaklanmamız gereken cümle 'Farklı sunum şekilleri olan nesneler' dir. Örnek olarak pide nesnesi için kıymalı pide, kaşarlı pide, sucuklu pide vb. bu nesnenin şekilleridir. 

 Bir örnekle devam edelim. Örnekte motorsiklet, otomobil, scooter gibi ürünler var. Tüm bu araçların istemci açısından kullanılabilir olması için üretim işleminde motorun, kapıların, viteslerin vb. parçalarında üretimi gerekmektedir.
 
Aslında bu ortak fonksiyonellikler bu ürünün hepsi için geçerlidir. Yani bu araçların hepsi bir Product olarak temsil edilebilir. İstemci, sadece kullanmak istediği ürünün farklı bir sunumunu elde etmek isteyecektir. Bu tip bir senaryoda istemcinin asıl ürüne ulaşmak için ele alması gereken üretim aşamalarından uzaklaştırılarak sadece üretmek istediği ürüne ait tipi bildirmesi yeterli olmalıdır. Bu senaryoda araç(Vehicle) aslında üründür(Product). Motorsiklet veya araba araç tipleridir. Bu senaryo pekela bir oyun programı içerisindeki araçların üretimi aşamasında göz önüne alınabilir. 

Builder pattern aktörleri aşağıdaki gibidir. 
---------------------------------------------
--> Builder : Product nesnesinin oluşturulması için gereken soyut arayüzü oluşturur. Interface veya abstract class olabilir. Concrete builder lar için parent niteliğindedir.

--> ConcreteBuilder : Builder arayüzünü kullanarak implemente edilen concrete builderlar, nesne tiplerini build eden concrete class lardır. Her bir concrete builder bir sunumu build eder. 

--> Product : Concrete builder lar tarafından üretilen client a sunulan product'tır.

--> Director : Kendisine parametre olarak verilen concreteBuilder nesnesi ile Builder arayüzü üzerinden client'a nesne oluşturan class'tır.

 */


using System;

CarDirector carDirector = new CarDirector();

CarBuilder carBuilder = new OpelConcreteBuilder();
carDirector.Product(carBuilder);
Console.WriteLine(carBuilder.Car.ToString());

carBuilder = new ToyotaConcreteBuilder();
carDirector.Product(carBuilder);
Console.WriteLine(carBuilder.Car.ToString());

// Product class
class Car
{
    public string BrandName { get; set; }
    public string SerieName { get; set; }

    public double Km { get; set; }
    public int Gear { get; set; }

    public override string ToString()
    {
        return $"{BrandName} marka araba {SerieName} modelinde {Km} kilometrede {Gear} vites olarak üretilmiştir.";
    }
}

// Builder class
abstract class CarBuilder
{
    protected Car _car;

    public Car Car { get { return _car; } }

    public abstract void SetBrand();
    public abstract void SetSerie();
    public abstract void SetKm();
    public abstract void SetGear();
}

// ConcreteBuilder class
class OpelConcreteBuilder : CarBuilder
{
    public OpelConcreteBuilder()
    {
        _car = new Car();
    }

    public override void SetBrand() => _car.BrandName = "Opel";

    public override void SetGear() => _car.Gear = 5;

    public override void SetKm() => _car.Km = 50000;

    public override void SetSerie() => _car.SerieName = "Corsa";
}

// ConcreteBuilder class
class ToyotaConcreteBuilder : CarBuilder
{
    public ToyotaConcreteBuilder()
    {
        _car = new Car();
    }
    public override void SetBrand() => _car.BrandName = "Toyota";

    public override void SetGear() => _car.Gear = 6;

    public override void SetKm() => _car.Km = 2100;

    public override void SetSerie() => _car.SerieName = "Corolla";
}

// ConcreteBuilder class
class BmwConcreteBuilder : CarBuilder
{
    public BmwConcreteBuilder()
    {
        _car = new Car();
    }
    public override void SetBrand() => _car.BrandName = "BMW";

    public override void SetGear() => _car.Gear = 7;

    public override void SetKm() => _car.Km = 200;

    public override void SetSerie() => _car.SerieName = "X7";
}

// director class
class CarDirector
{
    public void Product(CarBuilder carBuilder)
    {
        carBuilder.SetBrand();
        carBuilder.SetGear();
        carBuilder.SetSerie();
        carBuilder.SetKm();
    }
}