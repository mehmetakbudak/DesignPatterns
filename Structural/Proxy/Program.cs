/*
    Proxy tasarım deseni, oluşturulması karmaşık veya zaman alan işlemlerin kontrolünü sağlar. Proxy tasarım deseni çalışma maliyeti yüksek işlemlerin olduğu yapılarda, web servisi kullanılan yapılarda, remoting yapılarında, operasyonun gerçekleşmesinden öncek hazırlık yapılması ve ön işlem durumlarında kullanılır. 
    
    Kullanım senaryolarından biri için de bir class'ımız ve bunun bir nesnesi olduğunu varsayalım. Bu nesne bir okula ait tüm sınıf öğrencilerinin tüm bilgilerini tutuyor olsun. Aldıkları dersleri, derslerden aldıkları puanları, kaldıkları dersleri, isimleri, soyisimleri, kimlik bilgilerini ve her bir işlem yaptığımızda bu okul öğrencilerini bir tur dönüyor ve gerekli bilgileri içeinde barındırıyor olsun. Ancak client olarak tüm bilgilerden ziyade belli başlı sınıfların belli başlı özelliklerine ve bilgilerine ihitiyacımız olabilir. Bu durumda proxy class oluşturup sadece belli başlı metodlara ve üyelere erişim izni verebiliriz ve client nesneyi kopyaladığında orjinal nesneyi kopyalamasına kıyasla çok daha az bir maliyet ile işi halletmiş olur. 

    Aşağıdaki örnek uygulamamızın senaryosu resim gösterimi üzerine olsun. Gerçekte gösterilecek resimler büyük boyutta olsun ve kullanıcıya resmi gösterirken ilk sefer hariç resimler yüklenirken geçen zamanı save edebilmek için proxy class yazalım. 
    
    Uygulamada IImageGenerator adındaki interface bizim real subject'imiz ve proxy class'ımızın ortak bir dil konuşmasını sağlayacak yani bizim real subject'imiz de bu interface'i implemente edecektir.
 */

using System;

ImageGeneratorProxy proxy1 = new ImageGeneratorProxy("C:\\resim1.jpg");
ImageGeneratorProxy proxy2 = new ImageGeneratorProxy("C:\\resim2.jpg");

proxy1.ShowImage();
proxy2.ShowImage();
proxy1.ShowImage();

Console.ReadKey();

public interface IImageGenerator
{
    void ShowImage();
}

// Aşağıda ImageGenerator adında real subject'imiz vardır. Bu class esas işi yapacak olan class'tır. ImageGenerator'u implemente eder.
public class ImageGenerator : IImageGenerator
{
    private string _fullPath;
    public ImageGenerator(string fullPath)
    {
        _fullPath = fullPath;
    }

    public void ShowImage()
    {
        Console.WriteLine($"{_fullPath} resim gösteriliyor.");
    }
}

// Aşağıda ImageGeneratorProxy class'ımız vardır ve bu class da ImageGenerator'u implemente eder ve ImageGenerator classına vekillik eder. Görüldüğü üzere class'ın yaptığı gerçek class'a vekillik etmek ve ilgili özelliklerini kullanıcıya sunmaktır.
public class ImageGeneratorProxy : IImageGenerator
{
    private ImageGenerator _generator;
    private string _fullPath;
    public ImageGeneratorProxy(string fullPath)
    {
        _fullPath = fullPath;
    }

    public void ShowImage()
    {
        if (_generator == null)
        {
            _generator = new ImageGenerator(_fullPath);
        }
        _generator.ShowImage();
    }
}

