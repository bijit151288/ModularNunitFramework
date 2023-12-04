using CommonLibs.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPages.Pages
{
    public class BasePage
    {
        //Create an instance of CommonElements
        public CommonElement commonElement;

        //Create an instance of DropdownControl
        public DropdownControl dropdownControl;

        public BasePage()
        {
            //Initialization of commonElement
            commonElement = new CommonElement();

            //Initialization of dropdownControl
            dropdownControl = new DropdownControl();
        }
    }
}
