﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class InGamePreparationPhase : MonoBehaviour
{

    [SerializeField] private Button smallDecrementUIButton;
    [SerializeField] private Button mediumDecrementUIButton;
    [SerializeField] private Button largeDecrementUIButton;
    [SerializeField] private Button smallIncrementUIButton;
    [SerializeField] private Button mediumIncrementUIButton;
    [SerializeField] private Button largeIncrementUIButton;
    [SerializeField] private Button recipeDecrementMangoUIButton;
    [SerializeField] private Button recipeIncrementMangoUIButton;
    [SerializeField] private Button recipeDecrementGrahamUIButton;
    [SerializeField] private Button recipeIncrementGrahamUIButton;
    [SerializeField] private Button recipeDecrementMilkUIButton;
    [SerializeField] private Button recipeIncrementMilkUIButton;
    [SerializeField] private Button recipeDecrementIceCubesUIButton;
    [SerializeField] private Button recipeIncrementIceCubesUIButton;
    [SerializeField] private Button marketingDecrementPriceUIButton;
    [SerializeField] private Button marketingIncrementPriceUIButton;
    [SerializeField] private Button marketingDecrementAdvertisementUIButton;
    [SerializeField] private Button marketingIncrementAdvertisementUIButton;
    [SerializeField] private Button buyUIButton;
    [SerializeField] private Button cancelUIButton;
    [SerializeField] private Image popularityFillHUD;
    [SerializeField] private Image satisfactionFillHUD;
    [SerializeField] private Image smallSupplyHUD;
    [SerializeField] private Image mediumSupplyHUD;
    [SerializeField] private Image largeSupplyHUD;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private TextMeshProUGUI mangoUIText;
    [SerializeField] private TextMeshProUGUI grahamUIText;
    [SerializeField] private TextMeshProUGUI milkUIText;
    [SerializeField] private TextMeshProUGUI iceCubesUIText;
    [SerializeField] private TextMeshProUGUI cupsUIText;
    [SerializeField] private TextMeshProUGUI dateUIText;
    [SerializeField] private TextMeshProUGUI temperatureUIText;
    [SerializeField] private TextMeshProUGUI capitalUIText;
    [SerializeField] private TextMeshProUGUI bottomNavigationStateUIText;
    [SerializeField] private TextMeshProUGUI smallPriceUIText;
    [SerializeField] private TextMeshProUGUI smallQuantityUIText;
    [SerializeField] private TextMeshProUGUI mediumPriceUIText;
    [SerializeField] private TextMeshProUGUI mediumQuantityUIText;
    [SerializeField] private TextMeshProUGUI largePriceUIText;
    [SerializeField] private TextMeshProUGUI largeQuantityUIText;
    [SerializeField] private TextMeshProUGUI confirmationBuyUIText;
    [SerializeField] private TextMeshProUGUI mangoQuantityUIText;
    [SerializeField] private TextMeshProUGUI grahamQuantityUIText;
    [SerializeField] private TextMeshProUGUI milkQuantityUIText;
    [SerializeField] private TextMeshProUGUI iceCubesQuantityUIText;
    [SerializeField] private TextMeshProUGUI cupsPerPitcherUIText;
    [SerializeField] private TextMeshProUGUI priceUIText;
    [SerializeField] private TextMeshProUGUI advertisementUIText;
    [SerializeField] private Toggle resultsUINavButton;
    [SerializeField] private Toggle mangoUINavButton;
    [SerializeField] private ToggleGroup navigationPanel;
    [SerializeField] private ToggleGroup suppliesNavigationPanel;

    private enum InGamePreparationPhaseStates { idle, mainMenu, warningSave, confirmationBuy };
    private InGamePreparationPhaseStates inGamePreparationPhaseState = InGamePreparationPhaseStates.idle;
    private enum NavigationToRightStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToRightStates navigationToRightState;
    private enum NavigationToLeftStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToLeftStates navigationToLeftState;
    private NavigationToRightStates lastNavigationToRightState;

    private float[,] SUPPLIES_FLOAT;
    private int[,] RECIPE_INT;
    private int[,,] SUPPLIES_INT;

    private float capital;
    private float popularity;
    private float satisfaction;
    private float spend;
    private int suppliesState;
    private int mango;
    private int graham;
    private int milk;
    private int iceCubes;
    private int cups;
    private int temperature;
    private int cupsPerPitcher;
    private float price;
    private float advertisement;
    
    void Start()
    {

        SUPPLIES_INT = new int[5, 2, 3]
        {

            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } }

        };

        SUPPLIES_FLOAT = new float[5, 3]
        {

            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }

        };

        RECIPE_INT = new int[4, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 }

        };

        SUPPLIES_INT[0, 1, 0] = 12;
        SUPPLIES_INT[0, 1, 1] = 24;
        SUPPLIES_INT[0, 1, 2] = 48;
        SUPPLIES_INT[1, 1, 0] = 12;
        SUPPLIES_INT[1, 1, 1] = 20;
        SUPPLIES_INT[1, 1, 2] = 50;
        SUPPLIES_INT[2, 1, 0] = 12;
        SUPPLIES_INT[2, 1, 1] = 20;
        SUPPLIES_INT[2, 1, 2] = 50;
        SUPPLIES_INT[3, 1, 0] = 50;
        SUPPLIES_INT[3, 1, 1] = 200;
        SUPPLIES_INT[3, 1, 2] = 500;
        SUPPLIES_INT[4, 1, 0] = 75;
        SUPPLIES_INT[4, 1, 1] = 225;
        SUPPLIES_INT[4, 1, 2] = 400;

        SUPPLIES_FLOAT[0, 0] = 216.00f;
        SUPPLIES_FLOAT[0, 1] = 324.00f;
        SUPPLIES_FLOAT[0, 2] = 432.00f;
        SUPPLIES_FLOAT[1, 0] = 216.00f;
        SUPPLIES_FLOAT[1, 1] = 315.00f;
        SUPPLIES_FLOAT[1, 2] = 675.00f;
        SUPPLIES_FLOAT[2, 0] = 216.00f;
        SUPPLIES_FLOAT[2, 1] = 315.00f;
        SUPPLIES_FLOAT[2, 2] = 675.00f;
        SUPPLIES_FLOAT[3, 0] = 45.00f;
        SUPPLIES_FLOAT[3, 1] = 135.00f;
        SUPPLIES_FLOAT[3, 2] = 225.00f;
        SUPPLIES_FLOAT[4, 0] = 45.00f;
        SUPPLIES_FLOAT[4, 1] = 105.75f;
        SUPPLIES_FLOAT[4, 2] = 168.75f;

        RECIPE_INT[0, 1] = 20;
        RECIPE_INT[1, 1] = 10;
        RECIPE_INT[2, 1] = 10;
        RECIPE_INT[3, 1] = 7;

        FindObjectOfType<Player>().LoadPlayer();

        capital = FindObjectOfType<Player>().playerCapital;
        popularity = FindObjectOfType<Player>().currentPopularity;
        satisfaction = FindObjectOfType<Player>().currentSatisfaction;
        price = FindObjectOfType<Player>().price;
        advertisement = FindObjectOfType<Player>().advertisement;
        milk = FindObjectOfType<Player>().milkLeft;
        graham = FindObjectOfType<Player>().grahamLeft;
        milk = FindObjectOfType<Player>().milkLeft;
        iceCubes = FindObjectOfType<Player>().iceCubesLeft;
        cups = FindObjectOfType<Player>().cupsLeft;
        temperature = FindObjectOfType<Player>().currentTemperature;
        RECIPE_INT[0, 0] = FindObjectOfType<Player>().mangoPerServe;
        RECIPE_INT[1, 0] = FindObjectOfType<Player>().grahamPerServe;
        RECIPE_INT[2, 0] = FindObjectOfType<Player>().milkPerServe;
        RECIPE_INT[3, 0] = FindObjectOfType<Player>().iceCubesPerServe;

        navigationToRightState = NavigationToRightStates.results;
        navigationToLeftState = NavigationToLeftStates.results;
        lastNavigationToRightState = NavigationToRightStates.results;
        resultsUINavButton.isOn = true;
        mangoUINavButton.isOn = true;
        suppliesState = 0;

    }

    void Update()
    {

        FindObjectOfType<Player>().currentPopularity = popularity;
        FindObjectOfType<Player>().currentSatisfaction = satisfaction;
        FindObjectOfType<Player>().mangoLeft = mango;
        FindObjectOfType<Player>().grahamLeft = graham;
        FindObjectOfType<Player>().milkLeft = milk;
        FindObjectOfType<Player>().iceCubesLeft = iceCubes;
        FindObjectOfType<Player>().cupsLeft = cups;
        FindObjectOfType<Player>().currentTemperature = temperature;

        capitalUIText.text = string.Format("₱ {0}" , capital.ToString("0.00"));
        mangoUIText.text = mango.ToString();
        grahamUIText.text = graham.ToString();
        milkUIText.text = milk.ToString();
        iceCubesUIText.text = iceCubes.ToString();
        cupsUIText.text = cups.ToString();
        temperatureUIText.text = string.Format("Temp {0} °C", temperature.ToString());
        popularityFillHUD.fillAmount = popularity;
        satisfactionFillHUD.fillAmount = satisfaction;

        bottomNavigationStateUIText.text = GetBottomNavigationState(GetNavigation(navigationPanel));

        if (SimpleInput.GetButtonDown("OnMainMenu"))
        {

            OnAnimateFromInGamePreparationPhase(1);

        }

        if (SimpleInput.GetButtonDown("OnMainMenuAffirmative"))
        {

            OnAnimateFromInGamePreparationPhase(2);

        }

        if (SimpleInput.GetButtonDown("OnMainMenuNegative"))
        {

            OnAnimateFromInGamePreparationPhase(0);

        }

        if (SimpleInput.GetButtonDown("OnWarningSaveAffirmative"))
        {

            OnWarningSaveAffirmative();

        }

        if (SimpleInput.GetButtonDown("OnWarningSaveNegative"))
        {

            OnWarningSaveNegative();

        }

        if (SimpleInput.GetButtonUp("OnNavigation"))
        {

            OnNavigation();

        }

        if (SimpleInput.GetButtonUp("OnSuppliesNavigationMango"))
        {

            OnSuppliesNavigation(0);

        }

        if (SimpleInput.GetButtonUp("OnSuppliesNavigationGraham"))
        {

            OnSuppliesNavigation(1);

        }

        if (SimpleInput.GetButtonUp("OnSuppliesNavigationMilk"))
        {

            OnSuppliesNavigation(2);

        }

        if (SimpleInput.GetButtonUp("OnSuppliesNavigationIceCubes"))
        {

            OnSuppliesNavigation(3);

        }

        if (SimpleInput.GetButtonUp("OnSuppliesNavigationCups"))
        {

            OnSuppliesNavigation(4);

        }

        if (SimpleInput.GetButtonDown("OnIncrementSmall"))
        {

            OnSuppliesIncrement(0);

        }

        if (SimpleInput.GetButtonDown("OnIncrementMedium"))
        {

            OnSuppliesIncrement(1);

        }

        if (SimpleInput.GetButtonDown("OnIncrementLarge"))
        {

            OnSuppliesIncrement(2);

        }

        if (SimpleInput.GetButtonDown("OnDecrementSmall"))
        {

            OnSuppliesDecrement(0);

        }

        if (SimpleInput.GetButtonDown("OnDecrementMedium"))
        {

            OnSuppliesDecrement(1);

        }

        if (SimpleInput.GetButtonDown("OnDecrementLarge"))
        {

            OnSuppliesDecrement(2);

        }

        if (SimpleInput.GetButtonDown("OnCancel"))
        {

            if (cancelUIButton.interactable != false)
            {

                OnCancel();

            }

        }

        if (SimpleInput.GetButtonDown("OnBuy"))
        {

            if (buyUIButton.interactable != false)
            {

                spend = FindObjectOfType<Player>().playerCapital - capital;
                confirmationBuyUIText.text = string.Format("Are you sure you want to spend ₱ {0} on goods?", spend.ToString("0.00"));
                OnAnimateFromInGamePreparationPhase(3);

            }

        }

        if (SimpleInput.GetButtonDown("OnConfirmationBuyAffirmative"))
        {

            int countdown = 1;
            StartCoroutine(ConfirmationBuyAffirmativeToStart(countdown));
            OnConfirmationBuyNegative();

        }

        if (SimpleInput.GetButtonDown("OnConfirmationBuyNegative"))
        {

            OnConfirmationBuyNegative();

        }

        if (navigationToRightState == NavigationToRightStates.supplies)
        {     

            smallQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 0].ToString();
            mediumQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 1].ToString();
            largeQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 2].ToString();

            if (SUPPLIES_INT[suppliesState, 0, 0] == 0)
            {

                smallDecrementUIButton.interactable = false;  

            }
            else
            {

                smallDecrementUIButton.interactable = true;

            }

            if (SUPPLIES_INT[suppliesState, 0, 1] == 0)
            {

                mediumDecrementUIButton.interactable = false;

            }
            else 
            {

                mediumDecrementUIButton.interactable = true;

            }

            if (SUPPLIES_INT[suppliesState, 0, 2] == 0)
            {

                largeDecrementUIButton.interactable = false;

            }
            else
            {

                largeDecrementUIButton.interactable = true;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 0] >= 0)
            {
                
                smallIncrementUIButton.interactable = true;

            }
            else
            {

                smallIncrementUIButton.interactable = false;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 1] >= 0)
            {

                mediumIncrementUIButton.interactable = true;

            }
            else
            {

                mediumIncrementUIButton.interactable = false;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 2] >= 0)
            {

                largeIncrementUIButton.interactable = true;

            }
            else
            {

                largeIncrementUIButton.interactable = false;

            }

            if (FindObjectOfType<Player>().playerCapital != capital)
            {

                buyUIButton.interactable = true;
                cancelUIButton.interactable = true;

            }
            else 
            {

                buyUIButton.interactable = false;
                cancelUIButton.interactable = false;

            }

        }

        if (navigationToRightState == NavigationToRightStates.recipe)
        {

            FindObjectOfType<Player>().mangoPerServe = RECIPE_INT[0, 0];
            FindObjectOfType<Player>().grahamPerServe = RECIPE_INT[1, 0];
            FindObjectOfType<Player>().milkPerServe = RECIPE_INT[2, 0];
            FindObjectOfType<Player>().iceCubesPerServe = RECIPE_INT[3, 0];
            cupsPerPitcher = GetCupsPerPitcher(RECIPE_INT[3, 0]);

            mangoQuantityUIText.text = RECIPE_INT[0, 0].ToString();
            grahamQuantityUIText.text = RECIPE_INT[1, 0].ToString();
            milkQuantityUIText.text = RECIPE_INT[2, 0].ToString();
            iceCubesQuantityUIText.text = RECIPE_INT[3, 0].ToString();
            cupsPerPitcherUIText.text = string.Format("Cups per pitcher:\n{0}", cupsPerPitcher.ToString());

            if (SimpleInput.GetButtonDown("OnRecipeIncrementMango"))
            {

                OnRecipeIncrement(0);

            }

            if (SimpleInput.GetButtonDown("OnRecipeIncrementGraham"))
            {

                OnRecipeIncrement(1);

            }

            if (SimpleInput.GetButtonDown("OnRecipeIncrementMilk"))
            {

                OnRecipeIncrement(2);

            }

            if (SimpleInput.GetButtonDown("OnRecipeIncrementIceCubes"))
            {

                OnRecipeIncrement(3);

            }

            if (SimpleInput.GetButtonDown("OnRecipeDecrementMango"))
            {

                OnRecipeDecrement(0);

            }

            if (SimpleInput.GetButtonDown("OnRecipeDecrementGraham"))
            {

                OnRecipeDecrement(1);

            }

            if (SimpleInput.GetButtonDown("OnRecipeDecrementMilk"))
            {

                OnRecipeDecrement(2);

            }

            if (SimpleInput.GetButtonDown("OnRecipeDecrementIceCubes"))
            {

                OnRecipeDecrement(3);

            }

        }

        if (navigationToRightState == NavigationToRightStates.marketing)
        {

            FindObjectOfType<Player>().price = price;
            FindObjectOfType<Player>().advertisement = advertisement;
            priceUIText.text = price.ToString("0.00");
            advertisementUIText.text = advertisement.ToString("0.00");


            if (SimpleInput.GetButtonDown("OnMarketingDecrementPrice"))
            {

                OnPriceDecrement();

            }

            if (SimpleInput.GetButtonDown("OnMarketingIncrementPrice"))
            {

                OnPriceIncrement();

            }

            if (SimpleInput.GetButtonDown("OnMarketingDecrementAdvertisement"))
            {

                OnAdvertisementDecrement();

            }

            if (SimpleInput.GetButtonDown("OnMarketingIncrementAdvertisement"))
            {

                OnAdvertisementIncrement();

            }

        }

    }

    private void OnAnimateFromInGamePreparationPhase(int _inGamePreparationPhaseState)
    {

        inGamePreparationPhaseState = GetInGamePreparationPhaseState(_inGamePreparationPhaseState);
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("inGamePreparationPhaseState", (int) inGamePreparationPhaseState);

    }

    private InGamePreparationPhaseStates GetInGamePreparationPhaseState(int _inGamePreparationPhaseState)
    {

        return _inGamePreparationPhaseState switch
        {

            1 => InGamePreparationPhaseStates.mainMenu,

            2 => InGamePreparationPhaseStates.warningSave,

            3 => InGamePreparationPhaseStates.confirmationBuy,

            _ => InGamePreparationPhaseStates.idle,

        };

    }

    private void OnWarningSaveAffirmative()
    {

        FindObjectOfType<Player>().SavePlayer();
        OnWarningSaveNegative();

    }

    private void OnWarningSaveNegative()
    {

        OnAnimateFromInGamePreparationPhase(0);
        PlayerPrefs.SetInt("index", 1);
        SceneManager.LoadScene(0);

    }

    private void OnNavigation()
    {
        
        string navigation = GetNavigation(navigationPanel);
        navigationToRightState = GetNavigationToRight(navigation);
        navigationToLeftState = GetNavigationToLeft(navigation);

        // if (lastNavigationToRightState < navigationToRightState)
        if (lastNavigationToRightState != navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToRightState", (int) navigationToRightState);
            lastNavigationToRightState = navigationToRightState;
            mangoUINavButton.isOn = true;
            OnSuppliesQuantityClear();
            OnSuppliesNavigation(0);
            capital = FindObjectOfType<Player>().playerCapital;

        }
        /*else if (lastNavigationToRightState > navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToLeftState", (int) navigationToLeftState);
            lastNavigationToRightState = navigationToRightState;
            OnNavigateToSupplies();

        }*/

    }

    private NavigationToRightStates GetNavigationToRight(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => NavigationToRightStates.upgrades,

            "StaffUINavButton" => NavigationToRightStates.staff,

            "MarketingUINavButton" => NavigationToRightStates.marketing,

            "RecipeUINavButton" => NavigationToRightStates.recipe,

            "SuppliesUINavButton" => NavigationToRightStates.supplies,

            _ => NavigationToRightStates.results,

        };

    }

    private NavigationToLeftStates GetNavigationToLeft(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => NavigationToLeftStates.upgrades,

            "StaffUINavButton" => NavigationToLeftStates.staff,

            "MarketingUINavButton" => NavigationToLeftStates.marketing,

            "RecipeUINavButton" => NavigationToLeftStates.recipe,

            "SuppliesUINavButton" => NavigationToLeftStates.supplies,

            _ => NavigationToLeftStates.results,

        };

    }

    private string GetBottomNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => "Upgrades",

            "StaffUINavButton" => "Staff",

            "MarketingUINavButton" => "Marketing",

            "RecipeUINavButton" => "Recipe",

            "SuppliesUINavButton" => "Supplies",

            _ => "Results",

        };

    }

    private string GetNavigation(ToggleGroup _toggleGroup)
    {

        Toggle navigation = _toggleGroup.ActiveToggles().FirstOrDefault();
        return navigation.name.ToString();

    }

    private void OnSuppliesNavigation(int _suppliesNavigationState)
    {

        suppliesState = _suppliesNavigationState;

        smallSupplyHUD.sprite = resources[_suppliesNavigationState];
        mediumSupplyHUD.sprite = resources[_suppliesNavigationState];
        largeSupplyHUD.sprite = resources[_suppliesNavigationState];

        smallPriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 0].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 0].ToString("0.00"));
        mediumPriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 1].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 1].ToString("0.00"));
        largePriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 2].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 2].ToString("0.00"));

    }

    private void OnSuppliesDecrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        float price = SUPPLIES_FLOAT[suppliesState, _scale];

        if (SUPPLIES_INT[suppliesState, 0, _scale] - quantityPerPrice >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] -= quantityPerPrice;
            capital += price;

        }

    }

    private void OnSuppliesIncrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        float price = SUPPLIES_FLOAT[suppliesState, _scale];

        if (capital - price >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] += quantityPerPrice;
            capital -= price;

        }

    }

    private void OnSuppliesQuantityClear()
    {

        SUPPLIES_INT[0, 0, 0] = 0;
        SUPPLIES_INT[0, 0, 1] = 0;
        SUPPLIES_INT[0, 0, 2] = 0;
        SUPPLIES_INT[1, 0, 0] = 0;
        SUPPLIES_INT[1, 0, 1] = 0;
        SUPPLIES_INT[1, 0, 2] = 0;
        SUPPLIES_INT[2, 0, 0] = 0;
        SUPPLIES_INT[2, 0, 1] = 0;
        SUPPLIES_INT[2, 0, 2] = 0;
        SUPPLIES_INT[3, 0, 0] = 0;
        SUPPLIES_INT[3, 0, 1] = 0;
        SUPPLIES_INT[3, 0, 2] = 0;
        SUPPLIES_INT[4, 0, 0] = 0;
        SUPPLIES_INT[4, 0, 1] = 0;
        SUPPLIES_INT[4, 0, 2] = 0;

    }

    private string GetConjuctions(int _supply)
    {

        return _supply switch
        {

            0 => "mangoes = ₱",

            1 => "cups = ₱",

            2 => "cups = ₱",

            3 => "cubes = ₱",

            _ => "cups = ₱",

        };

    }

    private void OnCancel()
    {

        OnSuppliesQuantityClear();
        capital = FindObjectOfType<Player>().playerCapital;

    }
    private void OnConfirmationBuyNegative()
    {

        OnAnimateFromInGamePreparationPhase(0);
        OnSuppliesQuantityClear();

    }

    IEnumerator ConfirmationBuyAffirmativeToStart(int _countdown)
    {

        FindObjectOfType<Player>().playerCapital -= spend;

        mango += SUPPLIES_INT[0, 0, 0] + SUPPLIES_INT[0, 0, 1] + SUPPLIES_INT[0, 0, 2];
        graham += SUPPLIES_INT[1, 0, 0] + SUPPLIES_INT[1, 0, 1] + SUPPLIES_INT[1, 0, 2];
        milk += SUPPLIES_INT[2, 0, 0] + SUPPLIES_INT[2, 0, 1] + SUPPLIES_INT[2, 0, 2];
        iceCubes += SUPPLIES_INT[3, 0, 0] + SUPPLIES_INT[3, 0, 1] + SUPPLIES_INT[3, 0, 2];
        cups += SUPPLIES_INT[4, 0, 0] + SUPPLIES_INT[4, 0, 1] + SUPPLIES_INT[4, 0, 2];

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        OnConfirmationBuyNegative();

    }

    private void OnRecipeIncrement(int _recipe)
    {

        int maxQuantity = RECIPE_INT[_recipe, 1];

        if (RECIPE_INT[_recipe, 0] < maxQuantity)
        {

            RECIPE_INT[_recipe, 0]++;

        }

    }

    private void OnRecipeDecrement(int _recipe)
    {

        if (RECIPE_INT[_recipe, 0] != 0)
        {

            RECIPE_INT[_recipe, 0]--;

        }

    }

    private int GetCupsPerPitcher(int _iceCubes)
    {

        int cupsPerPitcher = 10;

        return _iceCubes switch
        {

            1 => cupsPerPitcher += 1,

            2 => cupsPerPitcher += 2,

            3 => cupsPerPitcher += 4,

            4 => cupsPerPitcher += 6,

            5 => cupsPerPitcher += 10,

            6 => cupsPerPitcher += 15,

            7 => cupsPerPitcher += 23,

            _ => cupsPerPitcher,

        };

    }

    private void OnPriceIncrement()
    {

        if (price + 0.1f < 5f)
        {
            
            price += 0.1f;

        }

    }

    private void OnPriceDecrement()
    {
        
        if (price > 0f)
        {

            price -= 0.1f;

        }

    }

    private void OnAdvertisementIncrement()
    {

        if (advertisement < 20f)
        {

            advertisement += 1f;

        }

    }

    private void OnAdvertisementDecrement()
    {

        if (advertisement > 0f)
        {

            advertisement -= 1f;

        }

    }

}
