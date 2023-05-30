/*
    State design pattern, nesnenin iç durumundaki değişikliklere göre çalışma zamanında farklı davranışlar sergilemesini sağlayan tasarım kalıbıdır. Yani biraz daha açarsak nesne belli özelliklerindeki değişimlere göre farklı davranışlar, fonksiyonellikler sergilemesi gibi düşünebiliriz.

    Gündelik hayattan bir örnekle devam edelim. Bir kredi kartınız olduğunu varsayalım ve bu kredi kartının durumlarına göre yani yazılım tarafından bakarsak iç özelliklerindeki değişime göre bankanız sizin hesabınızda değişiklikler yapıyor ve hesabınız farklı fonksiyonellikler sergiliyor.
    
    => Kredi kartını yeni açtıysanız banka size standart bir tarife sunuyor. (standart faizler, standart kredi çekebilme limitleri vb.)
    => Kredi kartınızda çok fazla borç birikti ve ödeyemiyorsanız hesabı kara listeye çevirip size kredi vermiyor belki de faizlerinizi arttırıyor.
    => Eğer borçlarınız zamanında ve düzenli ödüyorsanız da bir takım teşviklerin olduğu, reahatlıkla kredi alabileceğiniz bir premium hesaba çeviriyor.

    Yani kredi kartınızın bakiye-borç değerindeki değişimlere göre bankanız dinamik olarak sizin hesabınızı farklı davranışlar sergiliyor. Bu yapıyı normalde if-else koşul yapılarıyla kurabiliyor olsak da state pattern ile tanımlanan her state eski yapıyı değiştirmeden sisteme direk tak çıkar yani open closure principle'a uygun bir şekilde entegre olabilir.

    Örneğin Context class'ı değişik state'lerde farklı fonksiyonellik sergileyecek olan nesnemizdir. Bu sebeple state değişimlerini context class'ı takip eder ve yönetir. Yukarıdaki banka örneğinde bu kredi kartı ve borç bakiye bilgilerine karşılık gelir.

    Context tipinin tüm state'ler için ortak bir arayüz sunan başka bir tip ile ilişki kurması gerekir ki bu da soyut State class'ıdır. State class'ı değişik durumları temsil eden ortak arayüzdür. Bu arayüz sunumu asıl durum tipleri(Concrete state) tarafından değerlendirilebilir. Concrete stateler de (StateOne, StateTwo, StateThree) farklı durumları temsil eder. Kredi kartı örneği için bu anormal üyelik, kara liste, premium üyelik olarak düşünülebilir. 
 */


Account account = new Account();
account.WithdrawMoney();
account.PayInterest();

account.ChangeStatus(new GoldAccount());
account.WithdrawMoney();
account.PayInterest();

Console.ReadKey();

// soyut state nesnesi
public interface IAccountState
{
    void WithdrawMoney();
    void PayInterest();
}

public class GoldAccount : IAccountState
{
    public void PayInterest()
    {
        Console.WriteLine("Interest pay with golden account.");
    }

    public void WithdrawMoney()
    {
        Console.WriteLine("Money is taken with golden account.");
    }
}


public class NormalAccount : IAccountState
{
    public void PayInterest()
    {
        Console.WriteLine("Interest paid with normal account.");
    }

    public void WithdrawMoney()
    {
        Console.WriteLine("Money is taken with normal account.");
    }
}

public class Account
{
    private IAccountState _accountState;

    public Account()
    {
        _accountState = new NormalAccount();
    }

    public void PayInterest()
    {
        _accountState.PayInterest();
    }

    public void WithdrawMoney()
    {
        _accountState.WithdrawMoney();
    }

    public void ChangeStatus(IAccountState newAccountState)
    {
        _accountState = newAccountState;
    }
}
