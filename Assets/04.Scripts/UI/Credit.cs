using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Credit : MonoBehaviour, IStoreListener
{
    public Button AddCreditBtn;


    public readonly string ProductId = "01";

    private IStoreController _storeController;
    private IExtensionProvider storeExtensionProvider;

    private void Start()
    {
        InitUnityIAP();
        AddCreditBtn.onClick.AddListener(() => Purchase(ProductId));
    }

    private void InitUnityIAP()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(ProductId, ProductType.Consumable, new IDs() { { ProductId, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    private void Purchase(string productId)
    {
        Product product = _storeController.products.WithID(productId); // 상품정의

        if (product != null && product.availableToPurchase) // 상품이 존재하며 구매가 가능하면
        {
            _storeController.InitiatePurchase(product); //구매가 가능하면 진행
        }
        else
        {
            Debug.LogError("상품이 없거나 현재 구매가 불가능 합니다.");
        }
    }

    #region Interface

    /* 초기화 실패 시 실행되는 함수 */
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("초기화에 실패했습니다");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log(error + "/" + message);
    }

    /* 초기화 성공 시 실행되는 함수 */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        Debug.Log("초기화에 성공했습니다");

        _storeController = controller;
        storeExtensionProvider = extension;
    }

    /* 구매를 처리하는 함수 */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log("구매에 성공했습니다");

        if (args.purchasedProduct.definition.id == ProductId)
        {
            /*구매처리*/
        }

        return PurchaseProcessingResult.Complete;
    }


    /* 구매에 실패했을 때 실행되는 함수 */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("구매에 실패했습니다");
    }

    #endregion
}
