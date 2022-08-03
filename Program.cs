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

if (liquidLimit < 28)
{
    if (plasticityIndex < 8)
    {
        fineMaterial = "silt";
    }
    else
    {
        fineMaterial = "clay";
    }
}
else
{
    if (liquidLimit * 0.76 - 15.2 < plasticityIndex)
    {
        fineMaterial = "clay";
    }
    else
    {
        fineMaterial = "silt";
    }
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


Console.WriteLine("Your sample is classified as a:\n" + String.Join(" ", prefix) + prefixPrimarySeparator + primary + primarySecondarySeparator + String.Join(" and ", secondary) + secondaryTraceSeparator + String.Join(" and ", trace));