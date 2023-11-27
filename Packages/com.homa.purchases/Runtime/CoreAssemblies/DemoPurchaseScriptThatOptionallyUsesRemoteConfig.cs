using System.Collections;
using System.Collections.Generic;


#if HOMA_REMOTE_CONFIG
using Homa.Purchasing.RemoteConfig;  //local assembly where script that uses Remote config get enabled when Remote Config package is present
#endif


//This demo script will compile and run even if Homa Remote Config package is not installed in project. 

public class DemoPurchaseScriptThatOptionallyUesRemoteConfig
{

    public void DoSomePurchaseStuff()
    {
        //... LoadShop()
        // ..ValidatePastPurchases();
        //var RemoteParams = GetSomeRemoteParams;
        //if (RemoteParams != null)
        //{
            //Do stuff with Remote Params
            // E.g UpdatePrices()
     //   }
        //else
       // {
            //no remote params service avaiable in this project
            // ShowDefaultPrices()
       // }
    }
}
