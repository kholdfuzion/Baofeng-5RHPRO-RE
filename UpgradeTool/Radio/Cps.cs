namespace Radio;

public class Cps
{
    private static Cps cps;

    private static readonly object syncObj = new object();

    public TotModel TotModel { get; set; }

    public DtmfContactModel DtmfContactModel { get; set; }

    public DtmfBasicModel DtmfBasicModel { get; set; }

    public ButtonModel ButtonModel { get; set; }

    public BasicModel BasicModel { get; set; }

    public ScanBasicModel ScanBasicModel { get; set; }

    public SkipFrequencyModel SkipFrequencyModel { get; set; }

    public DtmfContactIndexModel DtmfContactIndexModel { get; set; }

    public ChannelModel ChannelModel { get; set; }

    public ChannelIndexModel ChannelIndexModel { get; set; }

    public ScanIndexModel ScanIndexModel { get; set; }

    public ModelModel ModelModel { get; set; }

    public TotModel TotModelDefault { get; set; }

    private Cps()
    {
        TotModel = new TotModel();
        DtmfContactModel = new DtmfContactModel();
        DtmfBasicModel = new DtmfBasicModel();
        ButtonModel = new ButtonModel();
        BasicModel = new BasicModel();
        ScanBasicModel = new ScanBasicModel();
        SkipFrequencyModel = new SkipFrequencyModel();
        DtmfContactIndexModel = new DtmfContactIndexModel();
        ChannelModel = new ChannelModel();
        ChannelIndexModel = new ChannelIndexModel();
        ScanIndexModel = new ScanIndexModel();
        ModelModel = new ModelModel();
    }

    public static Cps GetInstance()
    {
        if (cps == null)
        {
            lock (syncObj)
            {
                if (cps == null)
                {
                    cps = new Cps();
                }
            }
        }
        return cps;
    }

    public void Default()
    {
        TotModelDefault = (TotModel)TotModel.Clone();
    }
}
