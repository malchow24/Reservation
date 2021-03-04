// Payroll.cs
//
// 
// Payroll System Main Form.

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Payroll
{
    public partial class ReservationSystemForm : Form
    {
        public ReservationSystemForm()
        {
            InitializeComponent();

            // populate the state combobox
            foreach (string state in Address.StateNames)
                cmbState.Items.Add(state);

            // clear all fields
            clear();
        }

        private void PayrollSystemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the system?",
                "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void abroutPayrollSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payroll Information System V1.0, by Huimin Zhao",
                "About Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The printing function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void pageSetupStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The page setup function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The print preview function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstReservations.Items.Count == 0)
                return;

            // object for serializing Records in binary format
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream output = null; // stream for writing to a file

            DialogResult result;
            string fileName; // name of file to save data

            // create dialog box enabling user to save file
            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // allow user to create file
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }

            // exit event handler if user clicked "Cancel"
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invlaid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save file via FileStream if user specified valid file
            try
            {
                // open file with write access
                output = new FileStream(fileName,
                   FileMode.OpenOrCreate, FileAccess.Write);
                // save records to file
                foreach (Reservation item in lstReservations.Items)
                {
                    formatter.Serialize(output, item);
                }
            } // end try
            // handle exception if there is a problem opening the file
            catch (IOException)
            {
                // notify user if file does not exist
                MessageBox.Show("Error opening file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            // handle exception if there is a problem in serialization
            catch (SerializationException)
            {
                MessageBox.Show("Error writing to file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            finally
            {
                if (output != null)
                    output.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // object for deserializing Record in binary format
            BinaryFormatter reader = new BinaryFormatter();
            FileStream input = null; // stream for reading from a file

            
            DialogResult result;
            string fileName; // name of file containing data

            // create dialog box enabling user to open file
            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }


            // exit event handler if user clicked Cancel
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invalid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // create FileStream to obtain read access to file
                input = new FileStream(
                   fileName, FileMode.Open, FileAccess.Read);

                // read records from file
                lstReservations.Items.Clear();
                Reservation reservation = (Reservation)reader.Deserialize(input);
                while (reservation != null)
                {
                    lstReservations.Items.Add(reservation);
                    reservation = (Reservation)reader.Deserialize(input);
                }
            }
            catch
            {
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstReservations.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to update the following reservation?\n" +
                (Reservation)lstReservations.SelectedItem,
               "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if(add())
                lstReservations.Items.Remove(lstReservations.SelectedItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstReservations.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the following reservation?\n" +
                (Reservation)lstReservations.SelectedItem,
               "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                lstReservations.Items.Remove(lstReservations.SelectedItem);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void lstReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstReservations.SelectedItems.Count == 0)
            {
                return;
            }

            Reservation reservation = (Reservation)lstReservations.SelectedItem;
            clear();
            displayReservation(reservation);
        }

        // clear all fields
        private void clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            radMale.Checked = true;
            dtpCheckIn.Text = DateTime.Now.ToShortDateString();
            txtNights.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "Milwaukee";
            cmbState.Text = "WISCONSIN";
            txtZip.Text = "53201";
            txtHomePhone.Text = "(414)";
            txtMobilePhone.Text = "(414)"; 
            radEmployee.Checked = true;
            txtEmployeeID.Text = "";
            txtGuestType.Text = "";
            txtRoomCharge.Text = "";
            txtDiscountRate.Text = "";
            txtTax.Text = "";
        }

        // add a new reservation
        private bool add()
        {
            Reservation reservation;
            if (radEmployee.Checked)
                reservation = new EmployeeReservation();
            else if (radVIP.Checked)
                reservation = new VIPReservation();
            else
                reservation = new Reservation();

            if (getInputs(reservation) == true)
            {
                lstReservations.Items.Add(reservation);
                displayReservation(reservation);
                return true;
            }
            return false;
        }

        // get input fields of reservation
        private bool getInputs(Reservation reservation)
        {
            try
            {
                reservation.FirstName = txtFirstName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,	Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            try
            {
                reservation.LastName = txtLastName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            try
            {
                reservation.HomePhone = new PhoneNumber(txtHomePhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHomePhone.Focus();
                return false;
            }

            try
            {
                reservation.MobilePhone = new PhoneNumber(txtMobilePhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMobilePhone.Focus();
                return false;
            }

            reservation.IsMale = radMale.Checked;
            
            
            try
            {
                reservation.CheckIn = dtpCheckIn.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCheckIn.Focus();
                return false;
            }

            try
            {
                reservation.CheckOut = dtpCheckOut.Value;
                txtNights.Text = reservation.Nights.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCheckOut.Focus();
                return false;
            }

            if (radStandard.Checked)
                reservation.RoomType = (int)Reservation.RoomTypeEnum.STANDARD;
            else if (radDeluxe.Checked)
                reservation.RoomType = (int)Reservation.RoomTypeEnum.DELUXE;
            else
                reservation.RoomType = (int)Reservation.RoomTypeEnum.LUXURY;

            try
            {
                reservation.HomeAddress = new Address(txtStreet.Text, txtCity.Text, cmbState.Text, txtZip.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbState.Focus();
                return false;
            }

            if (reservation is EmployeeReservation)
            {
                try
                {
                    if (txtEmployeeID.Text == "")
                        txtEmployeeID.Text = "0";
                    ((EmployeeReservation)reservation).EmployeeID = int.Parse(txtEmployeeID.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmployeeID.Focus();
                    return false;
                }
            }
            else if(reservation is VIPReservation)
            {
                try
                {
                    ((VIPReservation)reservation).StartingDate = dtpStartingDate.Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpStartingDate.Focus();
                    return false;
                }
      
            }

            return true;
        }

        // display reservation
        private void displayReservation(Reservation reservation)
        {
            txtFirstName.Text = reservation.FirstName;
            txtLastName.Text = reservation.LastName;
            txtHomePhone.Text = reservation.HomePhone.ToString();
            txtMobilePhone.Text = reservation.MobilePhone.ToString();
            radMale.Checked = reservation.IsMale;
            radFemale.Checked = reservation.IsFemale;
            dtpCheckIn.Text = reservation.CheckIn.ToShortDateString();
            txtNights.Text = reservation.Nights.ToString();
            txtStreet.Text = reservation.HomeAddress.Street;
            txtCity.Text = reservation.HomeAddress.City;
            cmbState.Text = reservation.HomeAddress.State;
            txtZip.Text = reservation.HomeAddress.Zip;
            txtGuestType.Text = reservation.GuestType;
            txtRoomCharge.Text = reservation.RoomCharge.ToString("C");
            txtDiscountRate.Text = reservation.DiscountRate.ToString();
            txtTax.Text = reservation.Tax.ToString("C");

            switch (reservation.RoomType)
            {
                case (int)Reservation.RoomTypeEnum.STANDARD: radStandard.Checked = true;
                    break;
                case (int)Reservation.RoomTypeEnum.DELUXE: radDeluxe.Checked = true;
                    break;
                default:
                    radLuxury.Checked = true;
                    break;
            }
            if (reservation is EmployeeReservation)
            {
                radEmployee.Checked = true;
                txtEmployeeID.Text = ((EmployeeReservation)reservation).EmployeeID.ToString();
            }
           else if (reservation is VIPReservation)
            {
                radVIP.Checked = true;
                dtpStartingDate.Text = ((VIPReservation)reservation).StartingDate.ToShortDateString();
            }
            else
            {
                radOther.Checked = true;
            }
        }
    }
}
