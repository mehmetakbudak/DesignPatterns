/*
 Kalıtımsal ilişkileri olan nesnelerin üretilmesi amacıyla kullanılan desing pattern lerden birisidir. Burada asıl olan metottur. Bu metodun üstlendiği iş ise istemcinin ihtiyacı olan asıl ürünlerin üretilmesini sağlamaktır. 
 
Fabrika metodunun özelliği istemciden gelen talebe göre uygun olan ürünün üretilip istemciye verilmesidir. Tek sınıf ve metodun bu işi üstlenebilmesi için poliformik özelliği olan bir tipe ihtiyacımız var. Yani parent class tan türeyen sub classlar. Bu yüzden product ların interface olarak atası tanımlanır. Yani bizim creator class ımız bir product ı yani IProduct ın taşıyabileceği türden bir referansı geriye döndürecektir. 

Aşağıdaki örnekte Screen parent sınıfından 3 adet sub class oluşturulmuştur. Bu alt sınıfıların üretiminden sorumlu bir creator class (factory) tanımlanmıştır.
 
 */


// abstract parent class
using System;

Creator creator = new Creator();

var screenWindows = creator.ScreenFactory(ScreenType.Windows);
var screenWeb = creator.ScreenFactory(ScreenType.Web);  
var screenMobile = creator.ScreenFactory(ScreenType.Mobile);


screenWindows.Draw();
screenWeb.Draw();
screenMobile.Draw();




public abstract class Screen
{
    public abstract void Draw();
}


// concrete product
class WinScreen : Screen
{
    public override void Draw()
    {
        Console.WriteLine("Windows ekranı");
    }
}

// concrete product
class WebScreen : Screen
{
    public override void Draw()
    {
        Console.WriteLine("Web ekranı");
    }
}


// concrete product
class MobileScreen : Screen
{
    public override void Draw()
    {
        Console.WriteLine("Mobil ekranı");
    }
}

enum ScreenType
{
    Windows,
    Web,
    Mobile
}

class Creator
{
    public Screen ScreenFactory(ScreenType screenType)
    {
        Screen screen = null;
        switch (screenType)
        {
            case ScreenType.Windows:
                screen = new WinScreen();
                break;
            case ScreenType.Web:
                screen = new WebScreen();
                break;
            case ScreenType.Mobile:
                screen = new MobileScreen();
                break;
            default:
                break;
        }
        return screen;
    }
}

