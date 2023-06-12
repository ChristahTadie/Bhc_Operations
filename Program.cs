using System.Text.RegularExpressions;

namespace FarmerProcess
{
internal class Program
{
static void Main(string[] args)
{
Console.WriteLine("Hello, World!");
}

/*
*
The gross value of tobacco delivered is the sum of the value of the bales sold,
where the value of a bale is the product of its mass and price per kg

1. Create a function  CalculateGross that returns the gross given a list of bales (from same grower) sold and their prices.
*/
public double CalculateGross(List<Bale> bales)
{
double gross = 0;

foreach (var bale in bales)
gross += bale.Mass * bale.Price;

return gross;
}

/*
TAX_1 – 0.3% of gross value
TAX_2 – 1.5c per dollar worth of tobacco sold plus 2c per every kilogram of tobacco sold
    TAX_3 - $5.00 per bale of tobacco sold
*/

public double ProcessTaxes(double grossValue, List<Bale> balesSold)
{
double tax1 = grossValue * 0.003;
double tax2 = grossValue * 0.015 + balesSold.Sum(bale => bale.Mass) * 0.02;
double tax3 = balesSold.Count * 5;
double totalTax = tax1 + tax2 + tax3;

return grossValue - totalTax;
}

/*ProcessDebt function:* This function should take a debt item and its interest rate,
* calculate the total debt including interest, and deduct it from the gross value.
*/

public (double, double) ProcessDebt(double gross, Debt debt)
{
double total_debt = debt.Amount * (1 + debt.InterestRate);
double commission = total_debt * 0.005;

return (gross - total_debt - commission, commission);
}
/* ProcessDebts function:* This function should take a list of debt items, sort them by their priority, and process them using the ProcessDebt function.*/

public List<Debt> ProcessDebts(double gross, List<Debt> debts)
{
debts.Sort((x, y) => x.Priority.CompareTo(y.Priority));
double total_commission = 0;

foreach (var debt in debts)
{
var result = ProcessDebt(gross, debt);
gross = result.Item1;
total_commission += result.Item2;
}

return debts;
}
/*
ApplyRebate function:* This function should apply a given rebate to the gross value.
*/

public double ApplyRebate(double gross, List<Bale> bales, Rebate rebate)
{
double total_rebate = 0;

if (rebate.Type == "per_kg")
{
total_rebate = bales.Sum(bale => bale.Mass) * rebate.Rate;
}
else if (rebate.Type == "per_dollar")
{
total_rebate = gross * rebate.Rate;
}
else  // flat dollar amount
{
total_rebate = rebate.Amount;
}
return gross + total_rebate;
}
}


public class Rebate
{
public string Type { get; set; }
public int Rate { get; set; }
public double Amount { get; set; }

public Rebate Rebate1()
{

Rebate rebate1 = new Rebate();

rebate1.Type = "";
rebate1.Rate = 1;
rebate1.Amount = 1;

return rebate1;
}

public Rebate Rebate2()
{

Rebate rebate2 = new Rebate();
rebate2.Type = "";
rebate2.Rate = 1;
rebate2.Amount = 1;
return rebate2;
}

}
}

public class Bale
{
public double Mass { get; set; }
public double Price { get; set; }
public double Gross { get; set; }
public double GrowerNumber {get; set;}
public double Grade {get; set;}
public double BarCode {get; set;}

}


class Pallet
{
public string PalletNumber { get; set; }
public List<Bale> Bales { get; set; }
public string Warehouse { get; set; }
public List<string> Grades { get; set; }
}

class Debt
{
public float Amount { get; set; }
public int Priority { get; set; }
public float InterestRate { get; set; }
}
