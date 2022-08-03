// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Soil Classifier\n");

Console.WriteLine("What percentage of fine (<0.075mm) material is in your sample?");
double finePercent = Convert.ToDouble(Console.ReadLine());
string fineOrCoarse;

if (finePercent >= 35)
{
    Console.WriteLine("Your sample is fine grained.");
    fineOrCoarse = "fine";
}
else
{
    Console.WriteLine("Your sample is coarse grained.");
    fineOrCoarse = "coarse";
}

Console.WriteLine("What percentage of gravel (>2.36mm) material is in your sample?");
double gravelPercent = Convert.ToDouble(Console.ReadLine());

double sandPercent = 100 - gravelPercent - finePercent;

Console.WriteLine("You have " + Convert.ToString(sandPercent) + "% sand in your sample.");

Console.WriteLine("What is the plastic limit of your sample?");
double plasticLimit = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("What is the liquid limit of your sample?");
double liquidLimit = Convert.ToDouble(Console.ReadLine());

double plasticityIndex = liquidLimit - plasticLimit;

Console.WriteLine("The plasticity index of your sample is " + Convert.ToString(plasticityIndex));

string fineMaterial = "";
string plasticitySymbol = "";

switch (liquidLimit)
{
    case <= 26:
        if (plasticityIndex < 8)
        {
            fineMaterial = "silt";
            plasticitySymbol = "ML";

        }
        else
        {
            fineMaterial = "clay";
            plasticitySymbol = "CL";
        }
        break;
    case > 26 and <= 50:
        if (plasticityIndex < 0.73 * (liquidLimit - 20))
        {
            fineMaterial = "silt";
            plasticitySymbol = "ML";
        }
        else if (plasticityIndex > 0.73 * (liquidLimit - 20) && liquidLimit <= 35)
        {
            fineMaterial = "clay";
            plasticitySymbol = "CL";
        }
        else if (plasticityIndex > 0.73 * (liquidLimit - 20) && liquidLimit > 35)
        {
            fineMaterial = "clay";
            plasticitySymbol = "CI";
        }
        break;
    case > 50:
        if (plasticityIndex < 0.73 * (liquidLimit - 20))
        {
            fineMaterial = "silt";
            plasticitySymbol = "MH";
        }
        else if (plasticityIndex > 0.73 * (liquidLimit - 20))
        {
            fineMaterial = "clay";
            plasticitySymbol = "CH";
        }
        break;
    default:
        break;
}

Console.WriteLine("Your sample is predominantly a " + fineMaterial);

string primary = "";
string[] secondary = new string[] {};
string[] prefix = new string[] {};
string[] trace = new string[] { };

if (fineOrCoarse == "fine")
{
    primary = fineMaterial.ToUpper();

    switch (gravelPercent)
    {
        case <= 15:
            trace = trace.Append("gravel").ToArray();
            break;

        case > 15 and <= 30:
            secondary = secondary.Append("gravel").ToArray();
            break;

        case > 30:
            prefix = prefix.Append("gravelly").ToArray();
            break;

        default:
            break;
    }
    switch (sandPercent)
    {
        case <= 15:
            trace = trace.Append("sand").ToArray();
            break;

        case > 15 and <= 30:
            secondary = secondary.Append("sand").ToArray();
            break;

        case > 30:
            prefix = prefix.Append("sandy").ToArray();
            break;

        default:
            break;
    }
}
else if (fineOrCoarse == "coarse")
{

    switch (finePercent)
    {
        case <= 5:
            trace = trace.Append(fineMaterial).ToArray();
            break;

        case > 5 and <= 12:
            secondary = secondary.Append(fineMaterial).ToArray();
            break;

        case > 12:
            if (fineMaterial == "clay")
            {
                prefix = prefix.Append("clayey").ToArray();
            }
            else if (fineMaterial == "silt")
            {
                prefix = prefix.Append("silty").ToArray();
            }
            break;

        default:
            break;
    }

    if (gravelPercent >= sandPercent)
    {
        primary = "GRAVEL";

        switch (sandPercent)
        {
            case <= 15:
                trace = trace.Append("sand").ToArray();
                break;

            case > 15 and <= 30:
                secondary = secondary.Append("sand").ToArray();
                break;

            case > 30:
                prefix = prefix.Append("sandy").ToArray();
                break;

            default:
                break;
        }
    }
    else
    {
        primary = "SAND";

        switch (gravelPercent)
        {
            case <= 15:
                trace = trace.Append("gravel").ToArray();
                break;

            case > 15 and <= 30:
                secondary = secondary.Append("gravel").ToArray();
                break;

            case > 30:
                prefix = prefix.Append("gravelly").ToArray();
                break;

            default:
                break;
        }
    }

}


// Group Symbol Calculation
string groupSymbol = "";

void SymbolCalculation()
{
    switch (primary)
    {
        case "GRAVEL":
            switch (finePercent)
            {
                // TODO: Add coeficient of Uniformity and Curvature check
                case <= 5: groupSymbol = "GP";
                    break;

                case >5 and <12:
                    switch (fineMaterial)
                    {
                        case "clay":
                            groupSymbol = "GP-GC";
                            break;
                        case "silt":
                            groupSymbol = "GP-GM";
                            break;
                        default:
                            break;
                    }
                    break;

                case >= 12:
                    switch (fineMaterial)
                    {
                        case "clay": groupSymbol = "GC";
                            break;
                        case "silt": groupSymbol = "GM";
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            break;

        case "SAND":
            switch (finePercent)
            {
                // TODO: Add coeficient of Uniformity and Curvature check
                case <= 5:
                    groupSymbol = "SP";
                    break;

                case > 5 and < 12:
                    switch (fineMaterial)
                    {
                        case "clay":
                            groupSymbol = "SP-SC";
                            break;
                        case "silt":
                            groupSymbol = "SP-SM";
                            break;
                        default:
                            break;
                    }
                    break;

                case >= 12:
                    switch (fineMaterial)
                    {
                        case "clay":
                            groupSymbol = "SC";
                            break;
                        case "silt":
                            groupSymbol = "SM";
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            break;

        case "CLAY":
            groupSymbol = plasticitySymbol;
            break;

        case "SILT":
            groupSymbol = plasticitySymbol;
            break;

        default:
            break;
    }
}

SymbolCalculation();


string prefixPrimarySeparator = "";
string primarySecondarySeparator = "";
string secondaryTraceSeparator = "";

if (prefix.Length != 0)
{
    prefixPrimarySeparator = " ";
}

if (secondary.Length != 0)
{
    primarySecondarySeparator = " with ";
}

if(trace.Length != 0)
{
    secondaryTraceSeparator = ", trace ";
}


Console.WriteLine("Your sample is classified as a:\n" + "[" + groupSymbol + "] " + String.Join(" ", prefix) + prefixPrimarySeparator + primary + primarySecondarySeparator + String.Join(" and ", secondary) + secondaryTraceSeparator + String.Join(" and ", trace));