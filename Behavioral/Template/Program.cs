/*
    Template design pattern adından da anlaşılacağı gibi size bir şablon sunar. Bir akış ya da bir yapı düşünün. Arka arkaya ilerleyecek bir process. 
    Bu process'te belli aşamalar yahut basamaklar hep aynı iken belli kısımlar değişken olsun. İşte bu aynı olan kısımlar sizin değişken basamağınız için bir template yani şablon oluşturuyor.
    Peki burada amaç ne? Bana göre en önemli soru bu? Amaç bir iş için iskelet yani şablon bir algoritma ya da akış belirleyip bu prosesi uygulayacak tüm class'lara bu akışı uygulatmak. Implementasyon tarafında yapacağımız ise ana concrete sınıflarımızın türeyeceği bir adet soyut abstract class oluşturmak ve bu abstract class'ta tüm concrete class'lar için aynı olan fonksiyonellikleri direk olarak implemente etmek ancak tüm class'ların kendisine özgü implemente edeceği prosesleri de class'ın kendi içerisine bırakarak, parentta abstract metod olarak tanımlamak.Tabi bu ortak concrete metodlar ve class'ın kendi implemente edeceği metodlar arka arkaya bir process oluşturmalı ki bu da bir template metod üzerinden dışarıya açılabilsin.  Bu da bizlere aşağıdaki faydaları sağlar.   
    => Kod tekrarlarından kurtulmak
    => Maintenance'ı yüksek bir kod elde etmek
    => Akış süreci düzgün ve kolaylıkla değiştirilebilir bir süreç

    Bir örnekle anlamaya çalışalım. Örneğimizde bir alışveriş sepetimiz olsun ve bu alışveriş sepetine attığımız ürünlerin ödeme süreci olsun. Bu ödeme süreci genel olarak tüm ürünler için aynı olmakla birlikte ürün bazında bazı stepleri farklılık gösterebilir olacaktır ki bu da tam olarak template design pattern kullanmamız gereken senaryodur. Televizyon ve buzdolabı olan iki ürünümüzün satın alma süreci; başlangıç, ürün seçimi, ödeme ve bitiş olmak üzere her ürün için genel seçer 4 stepten oluşsun ve başlangıç ve bitiş tüm ürünler için ortak iken ürün seçimi ve ödeme şekli stepleri üründen ürüne farklılık göstersin. 
 */

using System;

// Main class'ta da Televizyon ve Buzdolabi class'ları örneklenip bize bir şablon sunan Template metot call edilmiştir. Bu template metodların çıktısı olarak, her class için ortak olan metotlar ortak sonuç getirecekken class'ın kendisine özgü olan metotlar ise kendi implemente ettikleri sonucu getirecektir. Yukarıda bahsedilen ana template akış prosesi aynı olacaktır.

Alisveris a1 = new Televizyon();
a1.TemplateMethod();

Console.WriteLine("**********************");

Alisveris a2 = new Buzdolabi();
a2.TemplateMethod();

Console.ReadKey();

enum OdemeTipi
{
    Pesin,
    Taksit
}

// Aşağıda alışveriş isimli class'ımız soyut ana class'ımız olup template i içerisinde barındırır. Görüldüğü üzere her alt class için ortak olacak Baslat ve Bitir metotları burada direkt olarak implemente edilmişken implementasyonu class'tan class'a değişecek olan Urun ve OdemeSekli metotları ise implementasyonu class'lara bırakılmak üzere abstract tanımlanmıştır. En sonda da TemplateMethod adında bir metotla bu concrete ve abstract metotların oluşturduğu process sıralı bir şekilde tüm class'ların kullanılması için sunulmuştur.

abstract class Alisveris
{
    protected string UrunAdi;

    protected OdemeTipi OdemeTipi;

    void Baslat()
    {
        Console.WriteLine("Alışveriş başladı");
    }

    void Bitir()
    {
        Console.WriteLine("Alışveriş bitti");
    }

    public abstract void Urun();

    public abstract void OdemeSekli();

    public void TemplateMethod()
    {
        Baslat();
        Urun();
        OdemeSekli();
        Bitir();
    }
}

// Televizyon class'ı abstract Alisveris class'ından türetilip kendine özgü olan işlemleri OdemeSekli ve Urun kendi içerisinde implemente edilmiştir.
class Televizyon : Alisveris
{
    public override void OdemeSekli()
    {
        OdemeTipi = OdemeTipi.Pesin;
        Console.WriteLine($"Ödeme Tipi => {OdemeTipi}");
    }

    public override void Urun()
    {
        UrunAdi = "Televizyon";
        Console.WriteLine($"Ürün Adı => {UrunAdi}");
    }
}

// Buzdolabi class'ı abstract Alisveris class'ından türetilip kendine özgü olan işlemleri OdemeSekli ve Urun kendi içerisinde implemente edilmiştir.
class Buzdolabi : Alisveris
{
    public override void OdemeSekli()
    {
        OdemeTipi = OdemeTipi.Taksit;
        Console.WriteLine($"Ödeme Tipi => {OdemeTipi}");
    }

    public override void Urun()
    {
        UrunAdi = "Buzdolabı";
        Console.WriteLine($"Ürün Adı => {UrunAdi}");
    }
}

