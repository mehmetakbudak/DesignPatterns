/*
Nesneleri new operatörüyle oluşturmak maliyetli olduğundan bu maliyeti azaltmayı amaçlayan tasarım kalıplarından biridir. Prototype desing pattern ın bize söylediği şey elinden bir nesnen var ise ve sen bu nesnenin birebir kopyasını  birçok kez daha yaratmak zorundaysan sıfırdan yaratma onun yerine elinde olan nesnenin kolanlarını al. Yani adından da anlaşılacağı üzere bir nesne prototip oluyor. Diğer nesneler de bu prototip üzerinden üretiliyor. İşin güzel yanı çoğu framework bize bir nesnenin clonu nu almak için hazır fonksiyonlar sunar. Bu da demektir ki prototype desing pattern i implemente ederken çok kod yazmaya gerek kalmıyor. Bu klonlama işleminde(kopyalama) deep-copy yöntemi kullanılıyor. Yani bir nesne birebir kopyalanarak yeni bir referans değişkene atanıyor. 

Aşağıdaki örnekte Product class ı içerisinde kendini clone layabilmesinin sağlanması için .net in bize sunduğu MemberwiseClone() fonksiyonu kullanılmıştır. Tek yapılması gereken class ın bir instance ını yaratmak ve sonrasında bu instance üzerinden clone fonksiyonunu kullanmaktır.
 
 */

using System;

var tv1 = new Product(1, "Lcd TV", 10000);
var tv2 = tv1.Clone() as Product;

Console.ReadKey();

abstract class AdventurePrototype
{
    public abstract AdventurePrototype Clone();
}

class Product : AdventurePrototype
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override AdventurePrototype Clone()
    {
        return this.MemberwiseClone() as AdventurePrototype;
    }
}
