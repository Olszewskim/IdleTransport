
using System;

[Serializable]
public class WarehouseData
{
    public BigInteger Capacity { get; private set; }
    public double CargoPackageProducingSpeed { get; private set; }
    public BigInteger TotalCargoAmountInPackage { get; private set; }

    public BigInteger AverageCargoProducedPerSecond { get; private set; }

    public BigInteger CurrentCargoAmount { get; private set; }

    private double _currentProductionCycle;

    public WarehouseData() {
        Capacity = Constants.WAREHOUSE_BASE_CAPACITY;
        CargoPackageProducingSpeed = Constants.WAREHOUSE_BASE_CARGO_PACKAGE_PRODUCING_SPEED;
        TotalCargoAmountInPackage = Constants.WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE;
        _currentProductionCycle = 0;
    }

    public void UpdateWarehouse(float deltaTime) {
        _currentProductionCycle += deltaTime;
        if (_currentProductionCycle >= CargoPackageProducingSpeed) {
            FinishCargoPackageProduction();
        }

    }

    private void FinishCargoPackageProduction() {
        _currentProductionCycle = 0;
        CurrentCargoAmount += TotalCargoAmountInPackage;
        if (CurrentCargoAmount > Capacity) {
            CurrentCargoAmount = Capacity;
        }
    }
}
