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
        Product product = _storeController.products.WithID(productId); // ��ǰ����

        if (product != null && product.availableToPurchase) // ��ǰ�� �����ϸ� ���Ű� �����ϸ�
        {
            _storeController.InitiatePurchase(product); //���Ű� �����ϸ� ����
        }
        else
        {
            Debug.LogError("��ǰ�� ���ų� ���� ���Ű� �Ұ��� �մϴ�.");
        }
    }

    #region Interface

    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("�ʱ�ȭ�� �����߽��ϴ�");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log(error + "/" + message);
    }

    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        Debug.Log("�ʱ�ȭ�� �����߽��ϴ�");

        _storeController = controller;
        storeExtensionProvider = extension;
    }

    /* ���Ÿ� ó���ϴ� �Լ� */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log("���ſ� �����߽��ϴ�");

        if (args.purchasedProduct.definition.id == ProductId)
        {
            /*����ó��*/
        }

        return PurchaseProcessingResult.Complete;
    }


    /* ���ſ� �������� �� ����Ǵ� �Լ� */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("���ſ� �����߽��ϴ�");
    }

    #endregion
}
