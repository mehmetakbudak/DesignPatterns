/*
    Composite tasarım deseni nesneleri ağaç yapısına göre düzenleyerek ağaç yapısındaki alt üst ilişkisini kurmaya çalışan yarayan bir desendir.Burada ağacın her bir yapısı ortak bir arayüzü uygular. Ortak bir arayüz uygulanmasının sebebi ise birbirine benzer yapılar ile çalışıyor olmak. Bu benzerlik bizlere esnek bir yapı sunacaktır.

    Client => İstemci sınıf
    Component => Soyut sınıfımızdır, özellikler içerisinde tanımlıdır ve diğer sınıflar bu yapıyı uygular.
    Composite => Alt öğeleri olan sınıfımızdır. Component' in somut halidir ve içerisinde Component listesi tutmaktadır.
    Leaf => Ağaç yapısındaki en alt elemanı temsil etmektedir.

    Senaryo olarak iç içe geçmiş kategorileri ve kategorilerin en altında bulunan ürünleri düşünebiliriz.
 */


// Soyut sınıfımızdır. Component kısmına denk gelmektedir. Diğer istemci sınıflar bu yapıyı uygular. Diğer sınıfların uygulayacağı hiyerarşiyi çizme metodunu uygular.
using System;
using System.Collections.Generic;

ProductCatalog samsung = new ProductCatalog("Samsung Telefonlar");
ProductCatalog products = new ProductCatalog("Ürünler");

Product iphone5Telefon = new Product("iPhone 5 Telefon");
ProductCatalog phones = new ProductCatalog("Telefonlar");
Product samsungGalaxyTelefon = new Product("Samsung Galaxy Telefon");

ProductCatalog iphone = new ProductCatalog("iPhone Telefonlar");


products.Add(phones);
phones.Add(iphone);
phones.Add(samsung);

iphone.Add(iphone5Telefon);
samsung.Add(samsungGalaxyTelefon);

products.DrawHierarchy();

Console.ReadKey();

interface ICatalogComponent
{
    void DrawHierarchy();
}

// Somut yapımızdır. Composite kısmına karşılık gelmektedir. Component(ICatalogComponent) listesi tutmaktadır.
class ProductCatalog : ICatalogComponent
{
    private string _name;
    private List<ICatalogComponent> _components;

    public ProductCatalog(string name)
    {
        _name = name;
        _components = new List<ICatalogComponent>();
    }

    public void Add(ICatalogComponent catalogComponent)
    {
        _components.Add(catalogComponent);
    }

    public void Remove(ICatalogComponent catalogComponent)
    {
        _components.Remove(catalogComponent);
    }

    public void DrawHierarchy()
    {
        Console.WriteLine(_name);
        foreach (ICatalogComponent component in _components)
        {
            component.DrawHierarchy();
        }
    }
}

// Somut yapımızdır. Leaf kısmına denk gelmektedir. Hiyerarşinin en alt tabakasını temsil eder. 
class Product : ICatalogComponent
{
    private string _name;
    
    public Product(string name)
    {
        _name = name;
    }

    public void DrawHierarchy()
    {
        Console.WriteLine(_name);    
    }
}
