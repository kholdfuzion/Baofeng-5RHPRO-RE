namespace Radio;

internal interface IViewData
{
    void InitView();

    void DataToView();

    void ViewToData();

    void LoadLanguageText(string section);
}
