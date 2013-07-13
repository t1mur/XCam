using System;
using System.Collections.Generic;
using System.Text;


namespace XCamera
{
    public class PixelFlyError
    {
        private static PixelFlyError instance = null;
        private static SortedDictionary<int, string> errorStrings;
        private PixelFlyError()
        {
            errorStrings = new SortedDictionary<int, string>();

            // Common Error
            errorStrings.Add(0, "No error. You should not see this. Bug in XCamera.");
            errorStrings.Add(-50, "Function not defined in supporting dll. Ask Widagdo.");
            errorStrings.Add(-51, "Board number is invalid. Ask Widagdo.");
            errorStrings.Add(-52, "Camera type not recognised. Ask Widagdo.");
            errorStrings.Add(-53, "Memory allocation failed. Ask Widagdo.");
            errorStrings.Add(-54, "Timeout in reading image.");
            errorStrings.Add(-55, "ROI or binnings inputs not valid.");
            errorStrings.Add(-56, "Supporting dll missing. You can try copying some dll or ask Widagdo.");
            errorStrings.Add(-57, "No driver found for this OS platform. Install the driver or ask Widagdo.");
            errorStrings.Add(-59, "Calling sequence error, not initialized. Ask Widagdo.");
            
            //PixelFly errors
            errorStrings.Add(-101,"Initialization failed, no camera connected.");
            errorStrings.Add(-102,"Timeout in something. I don't know where");
            errorStrings.Add(-103,"Function call with wrong parameter. Bug in program. Ask Widagdo.");
            errorStrings.Add(-104,"Cannot locate PCI card or card driver");
            errorStrings.Add(-105,"Wrong operating system.");
            errorStrings.Add(-106,"No or wrong driver installed");
            errorStrings.Add(-107,"IO function failed");
            errorStrings.Add(-108,"Call Cooke corp");
            errorStrings.Add(-109,"Invalid Camera Mode");
            errorStrings.Add(-110,"Call Cooke corp");
            errorStrings.Add(-111,"Device is hold by another process. Try close some program or restart the computer.");
            errorStrings.Add(-112,"Error in reading or writing data to board.");
            errorStrings.Add(-113,"Wrong driver function");

            errorStrings.Add(-201, "Timeout in any driver function");
            errorStrings.Add(-202, "Board is hold by another process");
            errorStrings.Add(-203, "Wrong boardtype");
            errorStrings.Add(-204, "Cannot match processhandle to a board");
            errorStrings.Add(-205, "Failed to init PCI");
            errorStrings.Add(-206, "No board found.");
            errorStrings.Add(-207, "Read configuration registers failed");
            errorStrings.Add(-208, "Board has wrong configuration");

            errorStrings.Add(-210, "Camera is busy");
            errorStrings.Add(-211, "Board is not idle");
            errorStrings.Add(-212, "Wrong parameter sent");
            errorStrings.Add(-213, "Head is disconnected");
            errorStrings.Add(-216, "Board initialization FPGA failed");
            errorStrings.Add(-217, "Board initialization NVRAM failed");
            errorStrings.Add(-221, "Not enough IO-buffer space for return values");
            errorStrings.Add(-230, "Picture buffer not prepared for transfer");
            errorStrings.Add(-231, "Picture buffer in use");
            errorStrings.Add(-232, "Picture buffer hold by another process");
            errorStrings.Add(-233, "Picture buffer not found");

            errorStrings.Add(-234, "Picture buffer cannot be freed");
            errorStrings.Add(-235, "Cannot allocate more picture buffer");
            errorStrings.Add(-236, "No memory left for picture buffer");
            errorStrings.Add(-237, "Memory reserve failed");
            errorStrings.Add(-238, "Memory commit failed");
            errorStrings.Add(-239, "Allocate internal memory LUT failed");
            errorStrings.Add(-240, "Allocate internal memory PAGETAB failed");

            errorStrings.Add(-248, "Event not available");
            errorStrings.Add(-249, "Delete event failed");

            errorStrings.Add(-256, "Enable interrupts failed");
            errorStrings.Add(-257, "disable interrupts failed");
            errorStrings.Add(-258, "No interrupt connected to the board");

            errorStrings.Add(-264, "Timeout in DMA");
            errorStrings.Add(-265, "No DMA buffer found");
            errorStrings.Add(-266, "Locking of pages failed");
            errorStrings.Add(-267, "Unlocking of pages failed");
            errorStrings.Add(-268, "DMA bufferesize to small");
            errorStrings.Add(-269, "PCI-BUS error in DMA");
            errorStrings.Add(-270, "DMA is running, command not allowed");

            errorStrings.Add(-328, "Get processor failed");
            errorStrings.Add(-329, "Call cooke");
            errorStrings.Add(-330, "Wrong processor found");
            errorStrings.Add(-331, "wrong processor size");
            errorStrings.Add(-332, "wrong processor device");
            errorStrings.Add(-333, "read flash failed");

        }

        public static string getErrorString(int errorID)
        {
            if (((errorID < -1) && (errorID > -33)) ||
                ((errorID >=100)&&(errorID <=104))
               )
            {
                return "This is Sensicam error. You don't even have the Camera. What did you do? " +
                        "Ask Widagdo please.";
                        
            }
            if (instance == null) instance = new PixelFlyError();
            string result;
            if (errorStrings.TryGetValue(errorID, out result))
            {
                return "Pixelfly Camera says this error: " + result;
            }
            else return "Unkown error code: " + errorID;
        }
    }
}
