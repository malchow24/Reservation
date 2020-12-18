// Address.cs
//
// Owen Malchow
// 
// Address class
using System;

[Serializable]
public class Address
{
    // private list of state names
    private static string[] stateNamesValue = {
        "ALABAMA",
        "ALASKA",
        "ARIZONA",
        "ARKANSAS",
        "CALIFORNIA",
        "COLORADO",
        "CONNECTICUT",
        "DELAWARE",
        "FLORIDA",
        "GEORGIA",
        "HAWAII",
        "IDAHO",
        "ILLINOIS",
        "INDIANA",
        "IOWA",
        "KANSAS",
        "KENTUCKY",
        "LOUISIANA",
        "MAINE",
        "MARYLAND",
        "MASSACHUSETTS",
        "MICHIGAN",
        "MINNESOTA",
        "MISSISSIPPI",
        "MISSOURI",
        "MONTANA",
        "NEBRASKA",
        "NEVADA",
        "NEW HAMPSHIRE",
        "NEW JERSEY",
        "NEW MEXICO",
        "NEW YORK",
        "NORTH CAROLINA",
        "NORTH DAKOTA",
        "OHIO",
        "OKLAHOMA",
        "OREGON",
        "PENNSYLVANIA",
        "RHODE ISLAND",
        "SOUTH CAROLINA",
        "SOUTH DAKOTA",
        "TENNESSEE",
        "TEXAS",
        "UTAH",
        "VERMONT",
        "VIRGINIA",
        "WASHINGTON",
        "WEST VIRGINIA",
        "WISCONSIN",
        "WYOMING"
    };

    // private list of state abbreviations
    private static string[] stateAbbreviationsValue = {
        "AL",
        "AK",
        "AZ",
        "AR",
        "CA",
        "CO",
        "CT",
        "DE",
        "FL",
        "GA",
        "HI",
        "ID",
        "IL",
        "IN",
        "IA",
        "KS",
        "KY",
        "LA",
        "ME",
        "MD",
        "MA",
        "MI",
        "MN",
        "MS",
        "MO",
        "MT",
        "NE",
        "NV",
        "NH",
        "NJ",
        "NM",
        "NY",
        "NC",
        "ND",
        "OH",
        "OK",
        "OR",
        "PA",
        "RI",
        "SC",
        "SD",
        "TN",
        "TX",
        "UT",
        "VT",
        "VA",
        "WA",
        "WV",
        "WI",
        "WY"
    };

    // private instance variable for storing street address
    private string streetValue;

    // private instance variable for storing city
    private string cityValue;

    // private instance variable for storing state index
    private int stateValue;

    // private instance variable for storing zip
    private string zipValue;

    // Parameter-less constructor
    public Address()
    {
    }

    // Constructor
    public Address(string street, string city, string state, string zip)
    {
        this.Street = street;
        this.City = city;
        this.State = state;
        this.Zip = zip;
    }

    // property to get and set street address
    public string Street
    {
        get
        {
            return streetValue;
        }
        set
        {
            streetValue = value.Trim();
        }
    }

    // property to get and set city
    public string City
    {
        get
        {
            return cityValue;
        }
        set
        {
            cityValue = Util.Capitalize(value.Trim());
        }
    }

    // property to get and set state name
    public string StateName
    {
        get
        {
            return Util.Capitalize(stateNamesValue[stateValue]);
        }
        set
        {
            // search in the lists of state names 
            string inputValue = value.Trim().ToUpper();
            for (int i = 0; i < stateNamesValue.Length; i++)
            {
                if (inputValue == stateNamesValue[i])
                {
                    stateValue = i;
                    return;
                }
            }
            throw new ApplicationException(value + " is not a U.S. state!");
        }
    }

    // property to get and set state, name or abbreviation
    public string State
    {
        get
        {
            return StateAbbreviation;
        }
        set
        {
            // search in the lists of state names and abbreviations
            value = value.Trim().ToUpper();
            for (int i = 0; i < stateNamesValue.Length; i++)
            {
                if (value == stateNamesValue[i] || value == stateAbbreviationsValue[i])
                {
                    stateValue = i;
                    return;
                }
            }
            throw new ApplicationException(value + " is not a U.S. state!");
        }
    }


    // property to get and set state abbreviation
    public string StateAbbreviation
    {
        get
        {
            return stateAbbreviationsValue[stateValue];
        }
        set
        {
            // search in the lists of state abbreviations
            value = value.Trim().ToUpper();
            for (int i = 0; i < stateNamesValue.Length; i++)
            {
                if (value == stateAbbreviationsValue[i])
                {
                    stateValue = i;
                    return;
                }
            }
            throw new ApplicationException(value + " is not a U.S. state!");
        }
    }

    // property to get and set zip
    public string Zip
    {
        get
        {
            return zipValue;
        }
        set
        {
            bool valid = true;
            value = value.Trim();
            if (value.Length != 5)
            {
                valid = false;
            }
            else
            {
                foreach (char digit in value)
                {
                    if (digit < '0' || digit > '9')
                    {
                        valid = false;
                        break;
                    }
                }
            }
            if (!valid)
            {
                throw new ApplicationException("Zip should consist of 5 digits!");
            }
            zipValue = value;
        }
    }

    // read-only property to get city and state abbreviation
    public string CityState
    {
        get
        {
            return City + ", " + StateAbbreviation;
        }
    }

    // static, read-only property to get the list of state abbreviations
    public static string[] StateAbbreviations
    {
        get
        {
            return stateAbbreviationsValue;
        }
    }

    // static, read-only property to get the list of state names
    public static string[] StateNames
    {
        get
        {
            return stateNamesValue;
        }
    }

    // returns string representation of Address object
    public override string ToString()
    {
        return Street + ", " + CityState + " " + Zip;
    } // end method ToString
}
