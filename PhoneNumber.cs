// PhoneNumber.cs
//
// Owen Malchow
// 
// PhoneNumber class
using System;

[Serializable]
public class PhoneNumber
{
	// private instance variable for storing the formatted phone number.
	private string phoneNumberValue = "";

    // read-only property to get area code
    public string AreaCode
    {
        get
        {
            return phoneNumberValue.Substring(0, 3);
        }
    }

	// returns string representation of PhoneNumber object.
	public override string ToString()
	{
        if (phoneNumberValue == "")
            return "";
        else
            return "("
                    + phoneNumberValue.Substring(0, 3)
                    + ")"
                    + phoneNumberValue.Substring(3, 3)
                    + "-"
                    + phoneNumberValue.Substring(6, 4); 
	}
		
	// Constructor
	public PhoneNumber(string phoneNumber)
	{
        // check whether string is empty.
        if (phoneNumber == "")
            return;
        // extract numeric digits
        foreach (char c in phoneNumber)
		{
			if (c >= '0' && c <= '9')
			{
                phoneNumberValue += c;
			}
		}
        // check length of phone number
        if (phoneNumberValue.Length != 10)
            throw new ApplicationException("Improper number of numeric digits for a phone number.");
	}
}