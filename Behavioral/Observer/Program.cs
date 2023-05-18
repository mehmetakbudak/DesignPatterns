/*
    Observer adından da anlaşılacağı üzere gözlemci, izleyici gözcü yahut gözetmen diye nitelendirilen, anlamı gibi işlev gören tasarım desenidir. Elimizde mevcut nesnenin durumunda herhangi bir değişiklik olduğundan, bu değişikliklerden diğer nesneleri haberdar eden bir tasarımdan  bahsediyoruz. Daha da net bir şekilde bahsetmek gerekirse, elimizdeki x nesnesinin y özelliğinde bir güncelleme, değişiklik yahut belirli bir şartın gerçekleşmesi gibi bir durum cereyan ettiğinde bu x nesnesini izleyen gözleyen diğer z, w, k vsç nesnelerine bu yeni durumu bildiren sisteme Observer tasarım deseni diyoruz.
    
    Observer tasarım deseninin klasik örnekleri arasında finans örneği gelir. Borsacılar borsadaki herhangi bir değişimden anında haberdar olmak ister. Finans kağıtlarındaki herhangi bir değişimden tüm borsacıları uyarmak mail yahut notfikasyon göndermek istiyorsak observer tasarım deseniyle finans kağıtlarını observe edip, gerekli bilgilendirmeleri yapabiliriz.

    Yukarıdaki finans örneğini kodlamadan önce aslında observer tasarım kalıbını implemente etmek için tüm yapmamız gereken, observe etmek istediğimiz özellik değiştiğinde listener' lara bildirim yollayabilmek için, ilgili property nin setter fonksiyonundan notifikasyon fonksiyonunu çağırmaktır. Böylece ilgili property ne zaman set edilirse akabinde fonksiyon da call edilecektir.

 */

// Subject : Takip etmek istediğimiz hisse stoku
using System;
using System.Collections.Generic;


Stock azonDemir = new Stock { Name = "Azon Demir Kimya", LotValue = 12.3M };

Financier xYatirim = new Financier { Name = "X Yatırım Şirketi" };

azonDemir.Subscribe(xYatirim); // xYatirimi güncelleme alabilmesi için abone ettik.

Financier zBank = new Financier { Name = "Z bank şirketi" };

azonDemir.Subscribe(zBank);

Console.WriteLine($"{azonDemir.Name} hissesinin güncel lot değeri {azonDemir.LotValue.ToString("c2")}");

azonDemir.LotValue += 1;

Console.ReadKey();

class Stock
{
    public Stock()
    {
        _financiers = new List<IFinancier>();
    }

    public string Name { get; set; }

    private List<IFinancier> _financiers;

    private decimal _lotValue;


    public decimal LotValue
    {
        get { return _lotValue; }
        set
        {
            _lotValue = value;
            Notify();
        }
    }

    private void Notify()
    {
        foreach (IFinancier financier in _financiers)
        {
            financier.Update(this);
            Console.WriteLine($"{financier.Name} bilgilendirildi.");
        }
    }

    public void Subscribe(IFinancier financier)
    {
        _financiers.Add(financier);
    }

    public void UnSubscribe(IFinancier financier)
    {
        _financiers.Remove(financier);
    }
}

//Observer : Gözlemcilerimiz
interface IFinancier
{
    string Name { get; set; }

    void Update(Stock stock);
}

class Financier : IFinancier
{
    public string Name { get; set; }
    public void Update(Stock stock)
    {
        Console.WriteLine($"{stock.Name} hissesinin lot değeri {stock.LotValue.ToString("c2")} olarak güncellendi.");
    }
}
