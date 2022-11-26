using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class PurchaseData
  {
    public List<BoughtIAP> BoughtIAPs = new List<BoughtIAP>();

    public event Action Changed; 

    public void AddPurchase(string Id)
    {
      BoughtIAP boughtIAP = Product(Id);

      if (boughtIAP != null)
        boughtIAP.Count++;
      else
        BoughtIAPs.Add(new BoughtIAP { IAPId = Id, Count = 1 });
      
      Changed?.Invoke();
    }

    private BoughtIAP Product(string Id) => 
      BoughtIAPs.Find(x => x.IAPId == Id);
  }
}