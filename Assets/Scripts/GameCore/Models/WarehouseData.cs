

public class WarehouseData
{
    public BigInteger Capacity { get; private set; }
    public BigInteger CargoPackageGeneratingSpeed { get; private set; }
    public BigInteger TotalCargoAmountInPackage { get; private set; }

    public BigInteger AverageCargoGeneratedPerSecond { get; private set; }

    public WarehouseData() {
        Capacity = Constants.WAREHOUSE_BASE_CAPACITY;
        CargoPackageGeneratingSpeed = Constants.WAREHOUSE_BASE_CARGO_PACKAGE_GENERATING_SPEED;
        TotalCargoAmountInPackage = Constants.WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE;
    }

}
