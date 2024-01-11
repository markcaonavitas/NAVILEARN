var TACDictionaryAsXmlString =
`
<!--
<GoiParameter>
  PropertyName 
    For example, PBTEMPC means Controller Temperature
  Address
  SubsetOfAddress
  Scale
  MemoryType:
    • ReadOrWrite
    • Flash
    • FlashReadOnly
  BitRangeStart
  BitRangeEnd
  MinimumParameterValue
  MaximumParameterValue
  ImplementedFirmwareVersion is used to specify whether new features are compatible with the presented firmware or not
  UserFrom
  UserTo
  DigitalFrom
  DigitalTo
  
  <EnumPointer> (Optional) is special tag which holds Property Name of the presented parameter's parent
  
  <RootEnumList> make its child components accessible to the parameter pointer 
  |  
  |  <RootEnumItem> is a 0-based index and selected base on the current parameter's value
  |  |  <NestedEnumListValue> (Optional)  Searches for the specified object and returns the zero-based index of the first occurrence,
  |  |                                  then apply the result to searh for a string from <NestedEnumListName>
  |  |  <NestedEnumListName> is similar to <enumListName> tag, it will flexibility over write enumListName
  |  |                      when the current parameter's value changes 
  |  |   <string> Option 1 </string> 
  |  |   <string> Option 2 </string>
  |  |  <NestedEnumListName>
  |  | <name> is a proper title for the parameter pointer
  |  </RootEnumItem>
  |   .
  |   .
  </RootEnumList>
  
  Name is a readable name which used to render the view
  Description was well explained in detail about the parameter, how it operated, and also included helpful troubleshotting notes
</GoiParameter>
-->
<ArrayOfGoiParameter xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <GoiParameter>
        <PropertyName>PBTEMPC</PropertyName>
        <Address>0</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Controller Temperature C</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VBATVDC</PropertyName>
        <Address>1</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DC Bus Voltage (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IBATADC</PropertyName>
        <Address>2</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Current</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IAA</PropertyName>
        <Address>3</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Ia (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IBA</PropertyName>
        <Address>4</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Ib (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ICA</PropertyName>
        <Address>5</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Ic (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Vdcbus_V_q7</PropertyName>
        <Address>6</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vdcbus_V_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Idc_Adc_q</PropertyName>
        <Address>7</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Idc_Adc_q</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>LogicPower_V_q7</PropertyName>
        <Address>8</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Logic Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>AccelDecelCoastStatusForDebugging</PropertyName>
        <Address>9</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Trajectory Segment</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OFFBRAKEJERKTIME10</PropertyName>
        <Address>10</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Off Brake Jerk Time 10</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DIRECTIONCHANGEACCELL</PropertyName>
        <Address>11</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Direction Change Acceleration</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VABVRMS</PropertyName>
        <Address>12</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Va (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VBCVRMS</PropertyName>
        <Address>13</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vb (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VCAVRMS</PropertyName>
        <Address>14</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vc (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VTHROTTLEV</PropertyName>
        <Address>15</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Throttle Voltage</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_throttle_footsw.png)
             The accelerator pedal under the floor board contains two circuits, one is this analog sensor where:
            <br><b>•</b> the below voltage thresholds are default programmable values, not usually changed but accessible through the throttle calibration routine.
            <br><b>•</b> below 1.5V pedal is usually fully off
            <br><b>•</b> above 3.7V is fully on
            <br><b>•</b> between 1.5V and 3.7V determines how hard the pedal is pressed.
            <br><b>•</b> See Foot Switch for second circuit.
			<br>
            <br><b>On:</b> above 1.5V
            <br><b>Not Pressed:</b> below 1.5V
            <br><b>Disconnected:</b> around 1.6V
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for throttle" and you are pressing on the throttle then this signal is preventing the vehicle from driving.
            <br><b>2.</b> Press the pedal and read the input in the Diagnostics Tab. If it is connected then the voltage will change and we need to check the voltage vs the throttle settings (double tap the actaul volage reading above to check the settings)
            <br><b>3.</b> If it is not connected then this input should read around 1.6V and you should check the vehicle harness
            <br><b>4.</b> If there is no change in voltage and you should check the vehicle harness.
            <br><b>5.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNALS BEGIN TO PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>6.</b> Checking the vehicle harness:
            <br><b>a)</b> Since this input needs to be connected to the controller to be powered by 5V you will have to disconnect the the 3 wire connector in the throttle assembly in the floor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signal wire should measure 1.6V since the throttle is disconnected.
            <br><b>d)</b> measure that there is continuity to Battery Ground on the ground wire.
 
            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_throttle_footsw.png)
             The accelerator pedal under the floor board contains two circuits, one is this analog sensor where:
            <br><b>•</b> the below voltage thresholds are default programmable values, not usually changed but accessible through the throttle calibration routine.
            <br><b>•</b> below 1.5V pedal is usually fully off
            <br><b>•</b> above 3.7V is fully on
            <br><b>•</b> between 1.5V and 3.7V determines how hard the pedal is pressed.
            <br><b>•</b> See Foot Switch for second circuit.
			<br>
            <br><b>On:</b> above 1.5V
            <br><b>Not Pressed:</b> below 1.5V
            <br><b>Disconnected:</b> around 1.6V
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for throttle" and you are pressing on the throttle then this signal is preventing the vehicle from driving.
            <br><b>2.</b> Press the pedal and read the input in the Diagnostics Tab. If it is connected then the voltage will change and we need to check the voltage vs the throttle settings (double tap the actaul volage reading above to check the settings)
            <br><b>3.</b> If it is not connected then this input should read around 1.6V and you should check the vehicle harness
            <br><b>4.</b> If there is no change in voltage and you should check the vehicle harness.
            <br><b>5.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNALS BEGIN TO PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>6.</b> Checking the vehicle harness:
            <br><b>a)</b> Since this input needs to be connected to the controller to be powered by 5V you will have to disconnect the the 3 wire connector in the throttle assembly in the floor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signal wire should measure 1.6V since the throttle is disconnected.
            <br><b>d)</b> measure that there is continuity to Battery Ground on the ground wire.

            Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>(Images:TAC2_Yamaha_G29.png)(ImageOverlays:_throttle_footsw.png)
             The accelerator pedal under the floor board contains two circuits, one is this analog sensor where:
            <br><b>•</b> the below voltage thresholds are default programmable values, not usually changed but accessible through the throttle calibration routine.
            <br><b>•</b> below 1.5V pedal is usually fully off
            <br><b>•</b> above 3.7V is fully on
            <br><b>•</b> between 1.5V and 3.7V determines how hard the pedal is pressed.
            <br><b>•</b> See Foot Switch for second circuit.
			<br>
            <br><b>On:</b> above 1.5V
            <br><b>Not Pressed:</b> below 1.5V
            <br><b>Disconnected:</b> around 1.6V
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for throttle" and you are pressing on the throttle then this signal is preventing the vehicle from driving.
            <br><b>2.</b> Press the pedal and read the input in the Diagnostics Tab. If it is connected then the voltage will change and we need to check the voltage vs the throttle settings (double tap the actaul volage reading above to check the settings)
            <br><b>3.</b> If it is not connected then this input should read around 1.6V and you should check the vehicle harness
            <br><b>4.</b> If there is no change in voltage and you should check the vehicle harness.
            <br><b>5.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNALS BEGIN TO PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>6.</b> Checking the vehicle harness:
            <br><b>a)</b> Since this input needs to be connected to the controller to be powered by 5V you will have to disconnect the the 3 wire connector in the throttle assembly in the floor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signal wire should measure 1.6V since the throttle is disconnected.
            <br><b>d)</b> measure that there is continuity to Battery Ground on the ground wire.

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
            <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png)(ImageOverlays:_throttle_footsw.png)
             The accelerator pedal under the floor board contains two circuits, one is this analog sensor where:
            <br><b>•</b> the below voltage thresholds are default programmable values, not usually changed but accessible through the throttle calibration routine.
            <br><b>•</b> below 1.5V pedal is usually fully off
            <br><b>•</b> above 3.7V is fully on
            <br><b>•</b> between 1.5V and 3.7V determines how hard the pedal is pressed.
            <br><b>•</b> See Foot Switch for second circuit.
			<br>
            <br><b>On:</b> above 1.5V
            <br><b>Not Pressed:</b> below 1.5V
            <br><b>Disconnected:</b> around 1.6V
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for throttle" and you are pressing on the throttle then this signal is preventing the vehicle from driving.
            <br><b>2.</b> Press the pedal and read the input in the Diagnostics Tab. If it is connected then the voltage will change and we need to check the voltage vs the throttle settings (double tap the actaul volage reading above to check the settings)
            <br><b>3.</b> If it is not connected then this input should read around 1.6V and you should check the vehicle harness
            <br><b>4.</b> If there is no change in voltage and you should check the vehicle harness.
            <br><b>5.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNALS BEGIN TO PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>6.</b> Checking the vehicle harness:
            <br><b>a)</b> Since this input needs to be connected to the controller to be powered by 5V you will have to disconnect the the 3 wire connector in the throttle assembly in the floor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signal wire should measure 1.6V since the throttle is disconnected.
            <br><b>d)</b> measure that there is continuity to Battery Ground on the ground wire.
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VBRAKEV</PropertyName>
        <Address>16</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Voltage</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_brake_brakesw.png)
             The brake pedal under the floor board contains two circuits, one is this analog sensor where:
            <br><b>•</b> the below voltage thresholds default programmable values, not usually changed or accessible.
            <br><b>•</b> below 1.0V pedal is usually fully off
            <br><b>•</b> above 3.5V is fully on
            <br><b>•</b> between 1.0V and 3.5V determines how hard the pedal is pressed.
            <br><b>•</b> See Brake Switch for second circuit.
			<br>
            <br><b>On:</b> above 1.0V
            <br><b>Not Pressed:</b> around 0.5V
            <br><b>Disconnected:</b> 5V
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for brake to release" and you are not pressing on the brake then this signal is preventing the vehicle from driving.
            <br><b>2.</b> Press the pedal and read the input in the Diagnostics Tab. If it is connected then the voltage will change.
            <br><b>3.</b> If it is not connected then this input should read around 1.6V and you should check the vehicle harness
            <br><b>4.</b> If there is no change in voltage and you should check the vehicle harness.
            <br><b>5.</b> Checking the vehicle harness:
            <br><b>a)</b> Since this input need to be connected to the controller to be powered by 5V you will have to disconnect the the 3 wire connector in the brake assembly in the floor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signal wire should measure 1.6V since the throttle is disconnected.
            <br><b>d)</b> measure that there is continuity to battery ground on the ground wire.

            <b>Description:</b> This whole section will not show up if connected to an actual vehicle and the analog brake is not enabled
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VPROG1V</PropertyName>
        <Address>17</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>V prog1 (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VPROG2V</PropertyName>
        <Address>18</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>V prog2 (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VPROG3V</PropertyName>
        <Address>19</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>V prog3 (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PBTEMPV</PropertyName>
        <Address>20</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PB temp (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MTTEMPV</PropertyName>
        <Address>21</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MT temp (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REVERSESW</PropertyName>
        <Address>22</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Off</string>
            <string>On</string>
        </enumListName>

        <Name>Reverse Switch</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_forward_reverse.png)
             This vehicle has two inputs to select a direction. Only one switch can be on at a time otherwise an error will disable the vehicle from moving
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates the controller is Waiting for Forward or Reverse if these signals are preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the the switch has battery voltage.
            
            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_forward_reverse.png)
             This vehicle has two inputs to select a direction. Only one switch can be on at a time otherwise an error will disable the vehicle from moving
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates the controller is Waiting for Forward or Reverse if these signals are preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the the switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>(Images:TAC2_Yamaha_G29.png)(ImageOverlays:_forward_reverse.png)
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
			<br>This vehicle has two inputs to select a direction. Only one switch can be on at a time otherwise an error will disable the vehicle from moving
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates the controller is Waiting for Forward or Reverse if these signals are preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the the switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
            <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png)(ImageOverlays:_forward_reverse.png)
             This vehicle has two inputs to select a direction. Only one switch can be on at a time otherwise an error will disable the vehicle from moving
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates the controller is Waiting for Forward or Reverse if these signals are preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the the switch has battery voltage.
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>COILFAULTIN</PropertyName>
        <Address>23</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Coil Fault In</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PROGIN</PropertyName>
        <Address>24</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <EnumPointer>InputOtfKeyOptions</EnumPointer>

        <Name>Pending</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FOOTSW</PropertyName>
        <Address>25</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Off</string>
            <string>On</string>
        </enumListName>

        <Name>Foot Switch</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_throttle_footsw.png)
             This input is one of two signals coming from the accelerator pedal. It is for redundant safety to ensure the accelerator pedal is functional. It must turn on before the controller uses the throttle voltage to drive.
            <br><b>•</b> This input is switched to battery voltage when the throttle is slightly pressed and before the throttle reaches the its on voltage.
            <br><b>•</b> The vehicle will not move if this signal is always on after power up or is off but the throttle voltage is on.
            <br><b>•</b> See Throttle Voltage for second circuit.
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for foot switch" then this signal is preventing the vehicle from driving.
            <br><b>2.</b> If the dashboard green words indicate "waiting for foot switch to release" then this signal is preventing the vehicle from driving.
            <br><b>3.</b> Press the pedal and read the input in the Diagnostics Tab.
            <br><b>4.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNALS BEGIN TO PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>5.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch Key off)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the accelerator pedal pressed and 0V when released.
            <br><b>d)</b> if c) fails open the throttle assembly under the accelerator pedal and verify the other side of the foot switch has battery voltage.
            <br><b>e)</b> Verify this switch closes when actuated.

            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_throttle_footsw.png)
             This input is one of two signals coming from the accelerator pedal. It is for redundant safety to ensure the accelerator pedal is functional. It must turn on before the controller uses the throttle voltage to drive.
            <br><b>•</b> This input is switched to battery voltage when the throttle is slightly pressed and before the throttle reaches the its on voltage.
            <br><b>•</b> The vehicle will not move if this signal is always on after power up or is off but the throttle voltage is on.
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for foot switch" then this signal is preventing the vehicle from driving.
            <br><b>2.</b> If the dashboard green words indicate "waiting for foot switch to release" then this signal is preventing the vehicle from driving.
            <br><b>3.</b> Press the pedal and read the input in the Diagnostics Tab.
            <br><b>4.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the accelerator pedal pressed and 0V when released.
            <br><b>d)</b> if c) fails open the Throttle assembly in the floor and verify the other side of the foot switch has battery voltage.
            <br><b>e)</b> Verify this switch closes when actuated.
            <br><b>d)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness

            Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>(Images:TAC2_Yamaha_G29.png)(ImageOverlays:_throttle_footsw.png)
             This input is one of two signals coming from the accelerator pedal. It is for redundant safety to ensure the accelerator pedal is functional. It must turn on before the controller uses the throttle voltage to drive.
            <br><b>•</b> This input is switched to battery voltage when the throttle is slightly pressed and before the throttle reaches the its on voltage.
            <br><b>•</b> The vehicle will not move if this signal is always on after power up or is off but the throttle voltage is on.
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for foot switch" then this signal is preventing the vehicle from driving.
            <br><b>2.</b> If the dashboard green words indicate "waiting for foot switch to release" then this signal is preventing the vehicle from driving.
            <br><b>3.</b> Press the pedal and read the input in the Diagnostics Tab.
            <br><b>4.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the accelerator pedal pressed and 0V when released.
            <br><b>d)</b> if c) fails open the Throttle assembly in the floor and verify the other side of the foot switch has battery voltage.
            <br><b>e)</b> Verify this switch closes when actuated.
            <br><b>d)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
            <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png)(ImageOverlays:_throttle_footsw.png)
             This input is one of two signals coming from the accelerator pedal. It is for redundant safety to ensure the accelerator pedal is functional. It must turn on before the controller uses the throttle voltage to drive.
            <br><b>•</b> This input is switched to battery voltage when the throttle is slightly pressed and before the throttle reaches the its on voltage.
            <br><b>•</b> The vehicle will not move if this signal is always on after power up or is off but the throttle voltage is on.
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for foot switch" then this signal is preventing the vehicle from driving.
            <br><b>2.</b> If the dashboard green words indicate "waiting for foot switch to release" then this signal is preventing the vehicle from driving.
            <br><b>3.</b> Press the pedal and read the input in the Diagnostics Tab.
            <br><b>4.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the accelerator pedal pressed and 0V when released.
            <br><b>d)</b> if c) fails open the Throttle assembly in the floor and verify the other side of the foot switch has battery voltage.
            <br><b>e)</b> Verify this switch closes when actuated.
            <br><b>d)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERA</PropertyName>
        <Address>26</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>7</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
            <string>2</string>
            <string>3</string>
            <string>4</string>
            <string>5</string>
            <string>6</string>
            <string>7</string>
        </enumListValue>
        <enumListName>
            <string>Changing  (Low)</string>
            <string>Changing (High)</string>
            <string>Not Changing  (Low)</string>
            <string>Not Changing (High)</string>
            <string>Fault  (Low)</string>
            <string>Fault (High)</string>
            <string>Fault  (Low)</string>
            <string>Fault (High)</string>
        </enumListName>

        <Name>Encoder A</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERB</PropertyName>
        <Address>27</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>7</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
            <string>2</string>
            <string>3</string>
            <string>4</string>
            <string>5</string>
            <string>6</string>
            <string>7</string>
        </enumListValue>
        <enumListName>
            <string>Changing  (Low)</string>
            <string>Changing (High)</string>
            <string>Not Changing  (Low)</string>
            <string>Not Changing (High)</string>
            <string>Fault  (Low)</string>
            <string>Fault (High)</string>
            <string>Fault  (Low)</string>
            <string>Fault (High)</string>
        </enumListName>

        <Name>Encoder B</Name>
        <Description>
            <![CDATA[
            <b>Description:</b>This vehicle has two inputs to determine motor speed. The controller provides 5V and ground to the motor speed sensor and reads back two signals, A and B which change between 0 and 5V at a rate and sequence depending on vehicle speed. They will typically change hundreds of times for one revolution of the motor.
			<br>
            <br><b>High:</b> 5V
            <br><b>Low:</b> 0V
            <br><b>Disconnected:</b> pulled to 5V internally ie., Always reading Not Changing (High).
            <br><b>Fault:</b> Not changing when motor has significant driving current

            <br>Troubleshooting Notes:
            <br><b>1.</b> Very small movement of the vehicle should shows these inputs Changing High and Low.
            <br><b>2.</b> After absolutely no movement for 5 seconds they will show Not Changing at whatever level they are at.
            <br><b>3.</b> If the motor has plenty of current to move and one or both of these signals are broken the motor may move very slowly until a fault occurs.
            <br><b>4.</b> To troubleshoot, IT IS BEST TO LIFT THE DRIVING WHEELS OFF THE GROUND, IF THE VEHICLE IS NOT IN NEUTRAL AND THESE SIGNAL ONLY PARTIALLY WORK THE VEHICLE MAY MOVE.
            <br><b>5.</b> With the vehicle in neutral these signals still function
            <br><b>6.</b> Checking the vehicle harness:
            <br><b>a)</b> Since these inputs need to be connected to the controller to be powered by 5V you will have to disconnect the 5 wire harness close to the Motor.
            <br><b>b)</b> With a meter measure that the 5V supply from the controller is on the 5V wire.
            <br><b>c)</b> The signals wires should measure 5V if the speed sensor is disconnected.
            <br><b>d)</b> measure that there is continuity to Battery Ground on the ground wire.
            <br><b>e)</b> if you can access the signals while plugged in to the controller then you should see them change from 0 to 5V while slightly moving the wheels.
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKESW</PropertyName>
        <Address>28</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Off</string>
            <string>On</string>
        </enumListName>

        <Name>Brake Switch</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_brake_brakesw.png)
            A normally closed switch in the brake pedal assembly which opens and forces the parking/emergency brake in the motor to engage COMPLETELY.
            <br><b>•</b> We cannot read this on a Danaher 23 PIN VEHICLE HARNESSES so it will say Off but it is connected to the actual brake.
            <br><b>•</b> on a 35 pin Curtis vehicle harness it is connected to the controller and indicates when the brake pedal has been fully pressed to the floor.
            <br><b>•</b> This wire is also connected directly to one side of the parking brake solenoid to power it. If this switch is open then this 48V is disconnected from the actual brake and the brake engages immediately whether or not the controller drives the Electric parking brake disengage output
			<br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> The controller does not do anything with this input but if your brake is never disengaging (see run/tow switch for help)
            <br><b>4.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch Key off)
            <br><b>b)</b> open the brake assembly under the brake pedal and verify the other side of the brake switch has battery voltage.
            <br><b>e)</b> Verify this switch closes when actuated.

            <b>Description:</b> This whole section will not show up if connected to an actual vehicle and the analog brake is not enabled
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FORWARDSW</PropertyName>
        <Address>29</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Off</string>
            <string>On</string>
        </enumListName>

        <Name>Forward Switch</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FAULTCODE</PropertyName>
        <Address>30</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Fault Code</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>JERKTIMEFORBRAKETOZEROVELOCITY31</PropertyName>
        <Address>31</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Braking to 0 Velocity Jerk 31</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SOFTWAREREVISION</PropertyName>
        <Address>32</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1000</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16.383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Firmware Revision</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IDA</PropertyName>
        <Address>33</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Id (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IQA</PropertyName>
        <Address>34</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Iq (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VDV</PropertyName>
        <Address>35</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vd (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAINISRCOUNTS</PropertyName>
        <Address>36</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>3.75</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4368.8</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Main ISR counts</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MTTEMPC</PropertyName>
        <Address>37</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Temperature C</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ROTORRPM</PropertyName>
        <Address>38</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor RPM</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>STATORHZ</PropertyName>
        <Address>39</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>StatorHz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Simulation20000msCounter</PropertyName>
        <Address>40</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Simulation20000msCounter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VelocityLoopSpeedRefInputRPM_q16</PropertyName>
        <Address>41</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity Command</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SPEEDCMDRPM</PropertyName>
        <Address>42</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>SPEEDCMDRPM</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IBATLIMIT</PropertyName>
        <Address>43</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>10</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>42</FlashOffset>

        <Name>Battery Discharge Limit (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>AMODULATION</PropertyName>
        <Address>44</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>A modulation</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BMODULATION</PropertyName>
        <Address>45</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>B modulation</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CMODULATION</PropertyName>
        <Address>46</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>C modulation</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OTWARNINGC</PropertyName>
        <Address>47</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Controller O/T warning (C)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CGAINAV</PropertyName>
        <Address>48</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>C gain A/V</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>INITPARAMS</PropertyName>
        <Address>49</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Init Params</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SAVEPARAMS</PropertyName>
        <Address>50</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Save Params</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FAULTRESET</PropertyName>
        <Address>51</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Fault Reset</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>UVTRIPV</PropertyName>
        <Address>52</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>UVTRIPV</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OVTRIPV</PropertyName>
        <Address>53</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>O/V Trip (V) </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OCTRIPA</PropertyName>
        <Address>54</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>O/C Trip (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OTTripC</PropertyName>
        <Address>55</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>O/T Trip (C)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKECT</PropertyName>
        <Address>56</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake CT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REVERSEBUZZER</PropertyName>
        <Address>57</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Reverse Buzzer</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKERES</PropertyName>
        <Address>58</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Res</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAINCT</PropertyName>
        <Address>59</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Main CT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TESTMODE</PropertyName>
        <Address>60</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Test Mode</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FREQ</PropertyName>
        <Address>61</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>400</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Open loop Freq (Hz)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MODINDEX</PropertyName>
        <Address>62</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Open loop Vq Modulation (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FLAGGATEENABLE</PropertyName>
        <Address>63</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Flag Gate Enable</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCPPR</PropertyName>
        <Address>64</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Enc PPR</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>POLEPAIRS</PropertyName>
        <Address>65</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Pole Pairs</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RATEDRPMCALC</PropertyName>
        <Address>66</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Rated RPM (calc)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RATEDFREQ</PropertyName>
        <Address>67</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4095.75</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Rated Freq</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RATEDVRMS</PropertyName>
        <Address>68</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Rated Vrms</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RATEDIRMS</PropertyName>
        <Address>69</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Rated Irms</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FWDLMTRPM</PropertyName>
        <Address>70</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>17</FlashOffset>

        <Name>Calculated Forward RPM Speed Limit</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> The actual RPM limit used internally by the TAC controller and is calculated from Forward Speed Limit, Tire Diameter and Rear Axle Ratio
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RVSLMTRPM</PropertyName>
        <Address>71</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>18</FlashOffset>

        <Name>Calculated Reverse RPM Speed Limit</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> The actual RPM limit used internally by the TAC controller and is calculated from Reverse Speed Limit, Tire Diameter and Rear Axle Ratio.
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RISERPMSEC</PropertyName>
        <Address>72</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Rise RPM/sec</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ScalerForMuchFasterNonVehicleAccelAndDecel_PUq0</PropertyName>
        <Address>73</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16384</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Accel and Decel Gain (pu)sec</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CURRENTKP</PropertyName>
        <Address>74</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Loop Kp</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Id_mod_index_q12</PropertyName>
        <Address>75</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Openloop Vd Modulation (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SPEEDKI</PropertyName>
        <Address>76</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Speed Loop Ki</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MSLIPVRMS</PropertyName>
        <Address>77</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Minimum Current</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MSLIPFREQHZ</PropertyName>
        <Address>78</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>10</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>M SlipFreq Hz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RISEHZSEC</PropertyName>
        <Address>79</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>101</FlashOffset>

        <Name>Maximum Acceleration</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FALLHZSEC</PropertyName>
        <Address>80</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Maximum Deceleration</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RSLIPVRMS</PropertyName>
        <Address>81</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-1.0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Magnitizing Minimum Current</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MSLIPGAIN</PropertyName>
        <Address>82</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>M slip gain</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SlipVoltage_Vrmsq4</PropertyName>
        <Address>83</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>12</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>SlipVoltage_Vrmsq4</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RSLIPFREQHZ</PropertyName>
        <Address>84</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>10</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>R SlipFreq Hz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SPEEDKP</PropertyName>
        <Address>85</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Speed Loop Kp</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FilteredSOC_q12</PropertyName>
        <Address>86</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40.96</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>State of Charge (%)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Soc_puq24</PropertyName>
        <Address>87</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Soc_puq24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>STATE</PropertyName>
        <Address>88</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>State</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>STATEINDEX</PropertyName>
        <Address>89</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>State Index</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>NONFATALOTTRIPC</PropertyName>
        <Address>90</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor O/T Warning (C)</Name>
        <Description>
            <![CDATA[
            <br><b>Description:</b>
            The controller will start to reduce motor torque When the motor temperature rises above Motor O/T Warning (C).
            The torque will be reduced to a minimum when the motor reaches the Motor O/T Trip (C) value.
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>KEYIN</PropertyName>
        <Address>91</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Off</string>
            <string>On</string>
        </enumListName>

        <Name>Key In</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <br><b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_key_run_charge.png)
             On this vehicle the key is the low current power supply for this controller. It does not power the Motor.
            <br><b>•</b> the controller B+ post provides high power to the motor, B+ is not directly connected internally to this input.
            <br><b>•</b> This input will precharge internal high power circuits with limited current before allowing the Main solenoid to close.
            <br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection
            <br>Troubleshooting Notes:
            <br><b>Scroll right through harness connectors above to find your type</b>
            <br><b>1.</b> If this pin is not connected to the battery then the controller will not show up in the Bluetooth list. The App will always show Key On
            <br><b>2.</b> Verify the red led on the controller comes on for 1 second and then turns off when the key is turned on
            <br><b>3.</b> If there are any diagnostic errors the red led will flash a code which can be decoded with this App.
            <br><b>4.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch Key off)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the key is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_key_run_charge.png)
             On this vehicle the key does not power this controller. The key is just a signal to the controller that the vehicle is enabled. The low current power supply for the controller comes from the Run/Tow switch under the seat.
            <br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>
            <br>Troubleshooting Notes:
            <br><b>Scroll right through harness images above to find your type</b>
            <br><b>1.</b> If the dashboard green words indicate "waiting for the key switch" then this input is off and preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the key is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>(Images:TAC2_Yamaha_G29.png)(ImageOverlays:_key_run_charge.png)
             On this vehicle the key does not power this controller. The key is just a signal to the controller that the vehicle is enabled. The low current power supply for the controller comes from the Run/Tow switch under the seat.
            <br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for the key switch" then this input is off and preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the key is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
            <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png)(ImageOverlays:_key_run_charge.png) On this vehicle the key does not power this controller. The key is just a signal to the controller that the vehicle is enabled. The low current power supply for the controller comes from the Run/Tow switch under the seat.
            <br>
            <br><b>On:</b> battery voltage
            <br><b>Off:</b> no connection, pulled to 0V internally
            <br>
            <br>Troubleshooting Notes:
            <br><b>1.</b> If the dashboard green words indicate "waiting for the key switch" then this input is off and preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to TOW)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the key is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.

        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEBRAKE</PropertyName>
        <Address>92</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Throttle Brake</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FORWARDSPEED</PropertyName>
        <Address>93</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Forward Speed</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TwoxPILsdq_PUq12</PropertyName>
        <Address>94</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TwoxPILsdq_PUq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEMIN</PropertyName>
        <Address>95</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>34</FlashOffset>

        <Name>Throttle Min (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEMAX</PropertyName>
        <Address>96</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>35</FlashOffset>

        <Name>Throttle Max (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKEOUTPUT</PropertyName>
        <Address>97</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CONFIGURINGTHROTTLE</PropertyName>
        <Address>98</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Configuring Throttle</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>EncoderPositionCounts</PropertyName>
        <Address>99</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Global Encoder Position Counts</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>KPBATTERY</PropertyName>
        <Address>100</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Kp Battery</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>NOTUSED1</PropertyName>
        <Address>101</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>NOTUSED1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>KiTerm</PropertyName>
        <Address>102</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>KiTerm</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ThrottleTerm</PropertyName>
        <Address>103</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ThrottleTerm</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DirLock</PropertyName>
        <Address>104</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DirLock</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PrevDirection</PropertyName>
        <Address>105</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PrevDirection</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOWIN</PropertyName>
        <Address>106</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <EnumPointer>DEBUGVAR1_TEST_NEW_PARAM_PAGE</EnumPointer>


        <Name>Pending</Name>


        <Description>
            <![CDATA[
    ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>CHARGERINTERLOCKIN</PropertyName>
        <Address>107</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
            <string>Connected</string>
            <string>Not Connected</string>
        </enumListName>
        <Name>Charger Interlock</Name>
        <Description>
            <![CDATA[
            Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_key_run_charge.png)
             When charger interlock reads Connected, the vehicle will be inhibited from driving to not damage the charger port.
            The charger receptical has four wires inside:
            <br><b>•</b> battery+(red) and battery-(black) for actual charging
            <br><b>•</b> the blue wire goes to the controller for this signal
            <br><b>•</b> grey wire goes to battery+ as well for actual charger usage
            <br>
            <br><b>Charger Connected:</b> 0v (broken wire or receptical can always read as Connected because the signal is pulled to 0V internally)
            <br><b>Charger Not Connected:</b> battery voltage
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates it is waiting for the charger to be removed if this signal is preventing the vehicle from driving.
            <br><b>2.</b> make sure the blue and grey wires are connected properly
            <br><b>3.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when charger is removed.
            <br><b>d)</b> if c) fails then verify with a meter that the charge port has has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_key_run_charge.png)
             When detected the vehicle will be inhibited from driving to not damage the charger port.
            The receptical has three wires inside:
            <br><b>•</b> battery+(red) and battery-(black) for actual charging
            <br><b>•</b> the third wire goes to the controller for this signal
            <br>
            <br><b>Charger Connected:</b> 0v (broken wire or receptical can always read as Connected because the signal is pulled to 0V internally)
            <br><b>Charger Not Connected:</b> battery voltage
            <br>Troubleshooting Notes:
            <br><b>1.</b> The App indicates it is waiting for the charger to be removed if this signal is preventing the vehicle from driving.
            <br><b>2.</b> make sure the 3 receptical wires are connected
            <br><b>3.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when charger is removed.
            <br><b>d)</b> if c) fails then verify with a meter that the charge port has has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A,Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>Not connected to Yamaha proprietary charge communications port.
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEADTIMECORRECTION</PropertyName>
        <Address>108</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Dead time correction</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Bootload</PropertyName>
        <Address>109</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Bootload</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SLIPREFSNAP1</PropertyName>
        <Address>110</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Torque Commanded </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SLIPREFSNAP2</PropertyName>
        <Address>111</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Torque Commanded after Limiters</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>StatorElectricalAngleOffset_PUq12</PropertyName>
        <Address>112</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>11.377777777777778</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1439.912109375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Stator Electrical Angle Offset(Deg)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>moving</PropertyName>
        <Address>113</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>moving</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PresentEncoderState</PropertyName>
        <Address>114</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PresentEncoderState</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MUXOUTPUT</PropertyName>
        <Address>115</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MUXOUTPUT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>WaringVector</PropertyName>
        <Address>116</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>WaringVector</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MOTTRIPC</PropertyName>
        <Address>117</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor O/T Trip (C)</Name>
        <Description>
            <![CDATA[
            <br><b>Description:</b>
            The controller will start to reduce motor torque When the motor temperature rises above Motor O/T Warning (C).
            The torque will be reduced to a minimum when the motor reaches the Motor O/T Trip (C) value .
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OVERCURRENTERRORS</PropertyName>
        <Address>118</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Over Current Errors</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PWMISR</PropertyName>
        <Address>119</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>60</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>273.05</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PWM ISR</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERISR</PropertyName>
        <Address>120</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>60</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>273.05</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder ISR</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VcmOneMs60MhzTicks</PropertyName>
        <Address>121</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>3.75</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4368.8</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vcm One Ms Ticks</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SLIPFREQHZ</PropertyName>
        <Address>122</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Slip Freq (Hz)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERTIMEOUTS</PropertyName>
        <Address>123</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder Timeouts cnt</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERDIRCHNGCNT</PropertyName>
        <Address>124</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder dir chng cnt</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERBADSEQCNT</PropertyName>
        <Address>125</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder bad seq cnt</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERISRCOUNTS</PropertyName>
        <Address>126</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder ISR counts</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>interface_rev_level</PropertyName>
        <Address>127</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>interface_rev_level</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DebugBatteryVoltage</PropertyName>
        <Address>128</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DebugBatteryVoltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DebugLogicPWRVoltage</PropertyName>
        <Address>129</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DebugLogicPWRVoltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VQV</PropertyName>
        <Address>130</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vq (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ROTORHZ</PropertyName>
        <Address>131</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RotorHz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>AGAINAV</PropertyName>
        <Address>132</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>A gain A/V</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CURRENTKI</PropertyName>
        <Address>133</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Loop Ki</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MuxSelect</PropertyName>
        <Address>134</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MuxSelect</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Speed_pu_q24</PropertyName>
        <Address>135</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Speed_pu_q24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PreviousGroupOneFaults</PropertyName>
        <Address>136</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PreviousGroupOneFaults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PreviousGroupTwoFaults</PropertyName>
        <Address>137</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PreviousGroupTwoFaults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PreviousGroupThreeFaults</PropertyName>
        <Address>138</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PreviousGroupThreeFaults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PreviousGroupFourFaults</PropertyName>
        <Address>139</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PreviousGroupFourFaults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>test5Torque_q12</PropertyName>
        <Address>140</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4092</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4.00366568914956</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>test5Torque_q12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Slip_Voltage_Vrms_q16_low</PropertyName>
        <Address>141</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Slip_Voltage_Vrms_q16_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>batteryVoltageLimiter</PropertyName>
        <Address>142</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Voltage Limiter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>NOTUSED3</PropertyName>
        <Address>143</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>NOTUSED3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Vdcbus_V_q23_local_filtered</PropertyName>
        <Address>144</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vdcbus_V_q23_local_filtered</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>slip_ref_puq24</PropertyName>
        <Address>145</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PID output (pu)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>slip_ref_puq24low</PropertyName>
        <Address>146</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>slip_ref_puq24low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VECTORCURRENT</PropertyName>
        <Address>147</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Current</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <!--this Parameter will replace VECTORCURRENT with a zero when FLAGGATEENABLE = 0
        for visual purposes-->
        <PropertyName>ZEROVECTORCURRENT</PropertyName>
        <Address>-1</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Current </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VdOut_puq24</PropertyName>
        <Address>148</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VdOut_puq24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PHASECURRENTLIMITER</PropertyName>
        <Address>149</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Phase Limiter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BATTERYCURRENTLIMITER</PropertyName>
        <Address>150</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Current Limiter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>KPPHASE</PropertyName>
        <Address>151</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Kp Phase</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>KPACCEL</PropertyName>
        <Address>152</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>45</FlashOffset>

        <Name>Throttle Positive Ramp Rate</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Options</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Manufacturing Configuration Option</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REVERSEENCODER</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>


        <enumListName>
          <string>Off</string>
          <string>On</string>
        </enumListName>
        <Name>Reverse Encoder Direction</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DISABLEANALOGBRAKE</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Disable Analog Brake Input</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DISABLEBRAKESWITCH</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Disable Brake Switch Input</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MFG_TESTING_ANALOG_BRAKE_AND_THROTTLE_OPTION</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Enable Network Throttle and Brake</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MANUFACTURINGTESTOPTION</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MANUFACTURING TEST OPTION (NEVER ENABLE)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DISABLEOFFTHROTTLEREGENTOSTOP</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Unimplented Mode</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REVERSEREARAXLEDIRECTION</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
          <string>Off</string>
          <string>On</string>
        </enumListName>
        <Name>Reverse Motor Axle Direction</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ANTIROLLBACK</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListName>
          <string>Off</string>
          <string>On</string>
        </enumListName>
        <Name>Reduce roll back when stopping</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DISABLEFOOTSWITCH</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Disable Foot Switch (NEVER ENABLE)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENABLENEUTRALSTARTUPINTERLOCK</PropertyName>
        <Address>153</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Enable Neutral Startup Interlock</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>SPEEDOMETERMAXSPEED</PropertyName>
        <Address>154</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>47</FlashOffset>

        <Name>Dashboard Display Speedometer Range MPH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TIREDIAMETER</PropertyName>
        <Address>155</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>64</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>50</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>48</FlashOffset>

        <Name>Tire Diameter(inches)</Name>
        <Description>
            <![CDATA[
            <b>Description:</b>
            Measure from the floor to the top of the tire (inches).
            (VisibleImages:TireSize.png)
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REARAXLERATIO</PropertyName>
        <Address>156</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>49</FlashOffset>

        <Name>Rear Axle Gear Ratio</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> The rear axle gear ratio is set to the default for this vehicle.
            <br/>Only changed with aftermarket gears.
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MILESORKILOMETERS</PropertyName>
        <Address>157</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <!--<Standard>-->
            <string>1</string>
            <!--<Metric>-->
        </enumListValue>
        <enumListName>
            <string>Standard</string>
            <!--0-->
            <string>Metric</string>
            <!--1-->
        </enumListName>

                <Name>Display Unit</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> Display Unit Option.            
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>HILLBRAKESTRENGTH</PropertyName>
        <Address>158</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>409.575</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Hill Brake Strength %</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>GROUPONEFAULTS</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GROUPONEFAULTS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ThrottleGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Throttle Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Throttle Fault - Vehicle is disabled
                <br/><b>Causes:</b>
                <br/><b>1.</b> The throttle is out of calibration.  Follow throttle calibration procedure.
                <br/><b>2.</b> If this error continues to appear then there may be broken wires in or to the throttle assembly
                <br/><b>3.</b> If the error only occurs when the throttle is pressed then the foot switch is stuck off or foot switch wires are broken
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Analog Brake Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Brake Pedal Voltage Fault
                <br/>Brake voltage is above default limit.
                <br/>Check brake pedal position.
                <br/>Check Brake Voltage on Diagnostics page.
                <br/>(1-2)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ChargerGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Charger Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Charger connection fault
                <br/>1. Unplug the charger                
                <br/>2. Check Diagnostic page to see that charger status changes when plugged in
                <br/>3. Press Ignore to allow this vehicle to still drive
                <br/>4. If batteries or charger have been replaced the charger interlock can be disabled on the Settings tab
                <br/>(1-3)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OverTempGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Controller Over Temperature Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Controller temperature is too high
                <br/>Performance will be limited until controller temperature drops
                <br/>(1-4)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorOverTempGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Over Temperature Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>
                <br/>Performance will be limited until motor temperature drops
                <br/>Motor temperature can be viewed on main Dashboard page
                <br/>If motor temperature is above 170C check wiring
                <br/>Motor may have alternate thermistor sensor.  See app setting.
                <br/>(1-5)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ContactorHighResGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Solenoid High Resistance Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> There is a large voltage drop across main solenoid contacts.
                <br/>Check for loose connections, burnt contacts or replace solenoid
                <br/>(1-6)
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ParameterTableInvalidGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Parameter Table Not Initialized</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Something has corrupted the vehicle configuration data. You will have to restore the factory defaults. Press the Restore button to initialize and save the default parameters.
                <br/>(1-7)
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>StartupBrakeMotionCheckGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Check Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Brake failed to hold vehicle still during start up test. Brake must be functional for vehicle operation.
                <br/>Vehicle should not move during brake check
                <br/>Turn key off and push vehicle.  Brake must hold vehicle in place.
                <br/>(1-8)
             ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RunningBrakeMotionCheckGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Hold Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Brake failed to hold vehicle still when stopped
                <br/>Wheels are still turning with Motor Brake set
                <br/>(1-9)
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RegenResistorCheckGroupOneFault</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Resistor Check Fault</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> The large external regen power resistor was not detected during start up tests.
                <br/>(1-10)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit10</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>10</BitRangeStart>
        <BitRangeEnd>10</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit10</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-11)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit11</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>11</BitRangeStart>
        <BitRangeEnd>11</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit11</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-12)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit12</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>12</BitRangeStart>
        <BitRangeEnd>12</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit12</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-13)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit13</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>13</BitRangeStart>
        <BitRangeEnd>13</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit13</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-14)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit14</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>14</BitRangeStart>
        <BitRangeEnd>14</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit14</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-15)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupOneFaultBit15</PropertyName>
        <Address>159</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>15</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit15</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Fault
                <br/>(1-16)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GROUPTWOFAULTS</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GROUPTWOFAULTS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DirectionGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Direction Switch Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Both the forward and reverse switches are on at the same time.
                <br/>Check Diagnostic page to verify switch operation
                <br/>(2-1)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ContactorNotClosedGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Solenoid Not Closed Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Solenoid doesn't appear to have closed
                <br/>Check solenoid operation
                <br/>(2-2)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PreChargeGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Pre Charge Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Controller has failed to pre-charge.
                <br/>Error can occur when accessories are miswired.
                <br/>Remove wires from B+ on controller and restart controller.  Check if fault persists.
                <br/>(2-3)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SolenoidGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Solenoid Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Main solenoid coil current is too high.
                <br/>Verify wiring, check connections or replace solenoid
                <br/>(2-4)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeSolenoidGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Solenoid Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Motor brake coil current is too high.
                <br/>Check resistance of motor brake.
                <br/>(2-5)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeRelayGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Light Relay Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Brake Light Relay current is too high.
                <br/>Check resistance of Brake Light Relay coil
                <br/>(2-6)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ReverseBuzzerGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Reverse Buzzer Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>Reverse buzzer current is too high
                <br/>Check for short at reverse buzzer
                <br/>(2-7)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>StartupPreChargeChangeGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Precharging too fast error</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>Precharge voltage monitor failed on startup
                <br/><b>1)</b>The main contactor may be welded closed
                <br/><b>2)</b>Or if a large power regen resistor is installed (RXV only) it is incorrectly connected to the battery side of the main contactor
                <br/>Disconnect the controller B+ post and/or the controller RES post, turn power off for 10 seconds then on to see if error changes
                <br/>(2-8)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>ParkBrakeTestContactorOpenGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Main Contactor Open While Attempting Parking Brake Test</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>The main solenoid was not closed during parking brake test
                <br/>Check main solenoid connections and that it is closing correctly.
                <br/>Verify that the controller firmware is updated.
                <br/>(2-9)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>CantCloseContactorGroupTwoFault</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.7</ImplementedFirmwareVersion>

        <Name>Main Solenoid Cannot Close Due To Insufficient Bus Capacitors Voltage</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b>The main solenoid was not closed due to the fact that the Bus Capacitors
				<br/>were not able to charge to a sufficiently high voltage.
                <br/>Check wiring connections to B+ post.
                <br/>Verify that the controller firmware is updated.
                <br/>(2-10)
				<br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit10</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>10</BitRangeStart>
        <BitRangeEnd>10</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupTwoBit10</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-11)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit11</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>11</BitRangeStart>
        <BitRangeEnd>11</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupTwoBit11</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-12)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit12</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>12</BitRangeStart>
        <BitRangeEnd>12</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupTwoBit12</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-13)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit13</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>13</BitRangeStart>
        <BitRangeEnd>13</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupTwoBit13</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-14)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit14</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>14</BitRangeStart>
        <BitRangeEnd>14</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupOneBit14</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-15)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupTwoFaultBit15</PropertyName>
        <Address>160</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>15</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupTwoBit15</Name>
        <!-- under voltage can cause this fault so don't show it -->
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>(2-16)
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GROUPTHREEFAULTS</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GROUPTHREEFAULTS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BusUnderVoltageGroupThreeFault</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Bus Under Voltage Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Voltage on controller B+ has dropped below (String.Format("{0:0}", UNDERVOLTAGETHRESHOLD))V
                <br/>Check batteries, solenoid and connections
                <br/>(3-1)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BusOverVoltageGroupThreeFault</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Bus Over Voltage Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Voltage on controller B+ too high
                <br/>Check wiring and solenoid.
                <br/>Check battery settings
                <br/>(3-2)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorOverCurrentGroupThreeFault</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Over Current Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-3)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>BrakeReleaseNotConnectedGroupThreeFault</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Release Not Connected Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Brake Release Output Not Connected
                <br/>Check wiring and connections.
                <br/>(3-4)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>APSDualThrottleMisMatchGroupThreeFault</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>APS Throttle Dual Output Mis-match</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> APS Throttle Dual Output Mis-Match
                <br/>Check wiring and connections to APS Throttle
                <br/>(3-5)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit5</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit5</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-6)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit6</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit6</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-7)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit7</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit7</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-8)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit8</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit8</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-9)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit9</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit9</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-10)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit10</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>10</BitRangeStart>
        <BitRangeEnd>10</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit10</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-11)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit11</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>11</BitRangeStart>
        <BitRangeEnd>11</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit11</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-12)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit12</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>12</BitRangeStart>
        <BitRangeEnd>12</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit12</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-13)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit13</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>13</BitRangeStart>
        <BitRangeEnd>13</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit13</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-14)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit14</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>14</BitRangeStart>
        <BitRangeEnd>14</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit14</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-15)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupThreeFaultBit15</PropertyName>
        <Address>161</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>15</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupThreeBit15</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented Error
                <br/>
                <br/>(3-16)
                <br/>
            ]]>
        </Description>
    </GoiParameter>


    <GoiParameter>
        <PropertyName>GROUPFOURFAULTS</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GROUPFOURFAULTS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareOverCurrentGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Over Current Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Hardware over current trip
                <br/>
                <br/>(4-1)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareOverVoltageGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Over Voltage Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Hardware over voltage trip.
                <br/>
                <br/>(4-2)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareUnderVoltageGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Under Voltage Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Hardware under voltage trip.
                <br/>
                <br/>(4-3)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareOverTempGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Over Temperature Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Hardware over temperature trip
                <br/>
                <br/>(4-4)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareMotorOverCurrentGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Current Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Hardware current fault
                <br/>Motor current has exceeded controller current limit
                <br/>Cycle power on controller.  If fault continues contact tech support
                <br/>(4-5)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>skipped1GroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>skipped1</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>Undocumented Error
                <br/>
                <br/>(4-6)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwarePowerStageFailureGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Power Stage Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareEncoderAGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder A input Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Speed Input A Not Changing when Motor Current Applied
                <br/>Check connections to speed encoder on motor
                <br/>(4-8)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareEncoderBGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder B input Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Speed Input B Not Changing when Motor Current Applied
                <br/>Check connections to speed encoder on motor
                <br/>(4-9)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ACFirmwareEncoderCGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder C input Fault</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Speed Input C Not Changing when Motor Current Applied
                <br/>Check connections to speed encoder on motor
                <br/>((4-10)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>EncoderRateLimitGroupFourFault</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>10</BitRangeStart>
        <BitRangeEnd>10</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Encoder Rate Limit Error</Name>
        <Description>
            <![CDATA[
                <b>Description:</b>Encoder signal interruption.  Reseat or replace encoder.
                <br/>
                <br/>(4-12)
                <br/>
            ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>GroupFourFaultBit11</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>11</BitRangeStart>
        <BitRangeEnd>11</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupFourBit11</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented error
                <br/>
                <br/>(4-12)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupFourFaultBit12</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>12</BitRangeStart>
        <BitRangeEnd>12</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupFourBit12</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented error
                <br/>
                <br/>(4-13)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupFourFaultBit13</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>13</BitRangeStart>
        <BitRangeEnd>13</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupFourBit13</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented error
                <br/>
                <br/>(4-14)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupFourFaultBit14</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>14</BitRangeStart>
        <BitRangeEnd>14</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupFourBit14</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented error
                <br/>
                <br/>(4-15)
                <br/>
            ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>GroupFourFaultBit15</PropertyName>
        <Address>162</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>15</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>GroupFourBit15</Name>
        <Description>
            <![CDATA[
                <b>Description:</b> Undocumented error
                <br/>
                <br/>(4-16)
                <br/>
            ]]>
        </Description>
    </GoiParameter>


    <GoiParameter>
        <PropertyName>VEHICLELOCKPASSWORD</PropertyName>
        <Address>163</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VEHICLELOCKPASSWORD</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VEHICLELOCKED</PropertyName>
        <Address>164</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VEHICLELOCKED</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RAWTHROTTLE</PropertyName>
        <Address>165</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Raw Throttle</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLECENTER</PropertyName>
        <Address>166</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Thottle Center</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEUPPER</PropertyName>
        <Address>167</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>THROTTLEUPPER</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLELOWER</PropertyName>
        <Address>168</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>THROTTLELOWER</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SERIALNUMBERHIGH</PropertyName>
        <Address>169</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Serial Number High</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SERIALNUMBERLOW</PropertyName>
        <Address>170</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Serial Number Low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>WRITESERIALNUMBER</PropertyName>
        <Address>171</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>WRITESERIALNUMBER</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SETSWITCHSTATE</PropertyName>
        <Address>172</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Set Switch States</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TESTERTHROTTLEANALOGVALUE</PropertyName>
        <Address>173</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4.5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Tester Throttle Analog Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TESTERBRAKEANALOGVALUE</PropertyName>
        <Address>174</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4.5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Tester Brake Analog Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SetOutputState</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>SetOutputState</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAINCTROUTPUT</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>>Main Contactor Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKERELAYOUTPUT</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Light Relay Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKECNTROUTPUT</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Parking Brake Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REVBUZZEROUTPUT</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Reverse Buzzer Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKERESOUTPUT</PropertyName>
        <Address>175</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Regen Resistor Output</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SLIPREFSNAP3</PropertyName>
        <Address>176</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Torque Limiter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>NOMINALBATTERYV</PropertyName>
        <Address>177</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>60</FlashOffset>

        <Name>Nominal Battery Voltage(36 or 48 or 72)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BOOTSLIPHZ</PropertyName>
        <Address>178</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>10</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Boost Slip Hz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKEMIN</PropertyName>
        <Address>179</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>62</FlashOffset>

        <Name>Brake Min (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKEMAX</PropertyName>
        <Address>180</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>5</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>63</FlashOffset>

        <Name>Brake Max (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BRAKEFULL</PropertyName>
        <Address>181</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>6</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>64</FlashOffset>

        <Name>Brake Fault (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEFULL</PropertyName>
        <Address>182</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>6</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>36</FlashOffset>

        <Name>Throttle Fault (Volts)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MOTORELECTRICALANGLEPU</PropertyName>
        <Address>183</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>0.71111111111111114</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>23038.59375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Motor Electrical Angle (Deg)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BATTERYRESISTANCE</PropertyName>
        <Address>184</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Resistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BATTERYCAPACITYAH</PropertyName>
        <Address>185</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Capacity (AH)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FULLSOCVOLTAGEATFULLCURRENT</PropertyName>
        <Address>186</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Full SOC Voltage at Full Current (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ZEROSOCVOLTAGEATFULLCURRENT</PropertyName>
        <Address>187</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Zero SOC Voltage at Full Current (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>LOGICSIDEBATTERYVOLTAGEV</PropertyName>
        <Address>188</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Voltage (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ANALOGANDREGENTORQUERATEPU1KHZ</PropertyName>
        <Address>189</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Analog and Regen Torque Rate (PU 1kHz)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLERAMPRATEPU1KHZ</PropertyName>
        <Address>190</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Throttle Negative Ramp Rate (PU 1kHz)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>HIGHSPEEDBRAKEMAXPUSLIP</PropertyName>
        <Address>191</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>High Speed Brake Max (PU slip)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>HIGHSPEEDBRAKEGAINPUSLIP</PropertyName>
        <Address>192</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>High Speed Brake Gain (PU slip)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEBUGVAR1</PropertyName>
        <Address>193</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Debug Var 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEBUGVAR2</PropertyName>
        <Address>194</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Debug Var 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ENCODERC</PropertyName>
        <Address>195</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>7</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <EnumPointer>SPEEDCRABBITTURTLE</EnumPointer>

        <Name>Pending</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PARAMETERVALID</PropertyName>
        <Address>196</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>FlashReadOnly</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Parameter Valid</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OTFPROG1MAX</PropertyName>
        <Address>197</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40.96</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>5</MinimumParameterValue>
        <MaximumParameterValue>100</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>73</FlashOffset>

        <Name>OTF Forward Speed (%)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OTFPROG2MAX</PropertyName>
        <Address>198</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40.96</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>2</MinimumParameterValue>
        <MaximumParameterValue>100</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>74</FlashOffset>

        <Name>OTF Off Throttle Brake Strength (%)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OTFPROG3MAX</PropertyName>
        <Address>199</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40.96</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>5</MinimumParameterValue>
        <MaximumParameterValue>100</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>75</FlashOffset>

        <Name>OTF Acceleration (%)</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> If the OTF is unlocked these inputs can be changed from 1% to 100% of maximum acceleration setting.
           
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SWITCHBITS</PropertyName>
        <Address>200</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>SWITCHBITS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MappedThrottle</PropertyName>
        <Address>201</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MappedThrottle</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IAinst_puq12</PropertyName>
        <Address>202</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>IAinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IBinst_puq12</PropertyName>
        <Address>203</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>IBinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ICinst_puq12</PropertyName>
        <Address>204</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ICinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VAinst_puq12</PropertyName>
        <Address>205</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VAinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VBinst_puq12</PropertyName>
        <Address>206</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VBinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VCinst_puq12</PropertyName>
        <Address>207</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VCinst_puq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MilesOrKmPerHourX10_q0</PropertyName>
        <Address>208</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1638.3</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MilesOrKmPerHourX10_q0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX100Miles_q0Low</PropertyName>
        <Address>209</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>100</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>163.83</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>OdometerX100Miles_q0Low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX100Miles_q0High</PropertyName>
        <Address>210</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>100</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>163.83</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>OdometerX100Miles_q0High</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAXIMUMREVERSERISERPM</PropertyName>
        <Address>211</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Maximum Reverse Rise RPM</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEBUGVAR3</PropertyName>
        <Address>212</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Debug Var 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEBUGVAR4</PropertyName>
        <Address>213</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Debug Var 4</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>COASTINGACCELRATE</PropertyName>
        <Address>214</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Coasting Accel Reduction Rate</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ANALOGBRAKEGAIN</PropertyName>
        <Address>215</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>78</FlashOffset>

        <Name>Analog Brake Gain</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VELOCITYERROR</PropertyName>
        <Address>216</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity Error</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPECH1SELECT</PropertyName>
        <Address>217</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPECH1SELECT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPECH2SELECT</PropertyName>
        <Address>218</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPECH2SELECT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPECH3SELECT</PropertyName>
        <Address>219</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPECH3SELECT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPECH4SELECT</PropertyName>
        <Address>220</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPECH4SELECT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETRIGGERLEVEL</PropertyName>
        <Address>221</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETRIGGERLEVEL</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETRIGGERMASK</PropertyName>
        <Address>222</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETRIGGERMASK</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPEPRECOUNT</PropertyName>
        <Address>223</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPEPRECOUNT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETIMEBASE</PropertyName>
        <Address>224</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETIMEBASE</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETRIGGERMODE</PropertyName>
        <Address>225</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETRIGGERMODE</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPECOMMAND</PropertyName>
        <Address>226</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPECOMMAND</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETRIGGERSTATE</PropertyName>
        <Address>227</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETRIGGERSTATE</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>dataScopeCh1ParamScale</PropertyName>
        <Address>228</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>dataScopeCh1ParamScale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>dataScopeCh2ParamScale</PropertyName>
        <Address>229</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>dataScopeCh2ParamScale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>dataScopeCh3ParamScale</PropertyName>
        <Address>230</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>dataScopeCh3ParamScale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>dataScopeCh4ParamScale</PropertyName>
        <Address>231</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>dataScopeCh4ParamScale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DATASCOPETRIGGERADDRESS</PropertyName>
        <Address>232</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>DATASCOPETRIGGERADDRESS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Slip_Voltage_Vrms_q16</PropertyName>
        <Address>233</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Slip_Voltage_Vrms_q16</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VELOCITYTOTORQUEMODECROSSOVERRPM</PropertyName>
        <Address>234</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity To Torque Mode Crossover RPM</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VelocityIntegralTerm</PropertyName>
        <Address>235</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VelocityIntegralTerm</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ONBRAKEJERK</PropertyName>
        <Address>236</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>On Brake Jerk(ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OFFTHROTTLEJERK237</PropertyName>
        <Address>237</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Off Throttle Jerk 237</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>THROTTLEKNEEGAIN238</PropertyName>
        <Address>238</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Throttle Knee Gain 238</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>NEWCONTROLOOPS</PropertyName>
        <Address>239</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>New Control Loop bits</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
      <PropertyName>NEW_FOC_ENABLE</PropertyName>
      <Address>239</Address>
      <SubsetOfAddress>true</SubsetOfAddress>
      <Scale>1</Scale>
      <MemoryType>Flash</MemoryType>
      <BitRangeStart>8</BitRangeStart>
      <BitRangeEnd>8</BitRangeEnd>
      <MinimumParameterValue>0</MinimumParameterValue>
      <MaximumParameterValue>1</MaximumParameterValue>
      <ImplementedFirmwareVersion>9.1</ImplementedFirmwareVersion>

      <Name>Deceleration Improvement Experiment</Name>
      <Description>
        <![CDATA[
              <b>Description:</b> Enable a motor control method that improves linearity of deceleration.
            ]]>
      </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OVERVOLTAGETHRESHOLD</PropertyName>
        <Address>240</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Over Voltage Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>UNDERVOLTAGETHRESHOLD</PropertyName>
        <Address>241</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>100</FlashOffset>

        <Name>Under Voltage Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>InvertDigitalInput</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Invert Digital Input</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEKEYSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEKEYSWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLETOWSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLETOWSWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLECHARGERSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Invert charger input</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEFORWARDSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>3</BitRangeStart>
        <BitRangeEnd>3</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEFORWARDSWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEREVERSESWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>4</BitRangeStart>
        <BitRangeEnd>4</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEREVERSESWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEFOOTSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>5</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEFOOTSWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEBRAKESWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>6</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEBRAKESWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEOTFSWITCH</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEOTFSWITCH</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLESOLENOIDFAULT</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>8</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLESOLENOIDFAULT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEBRAKEFAULT</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>9</BitRangeStart>
        <BitRangeEnd>9</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEBRAKEFAULT</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEENCODERA</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>10</BitRangeStart>
        <BitRangeEnd>10</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEENCODERA</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TOGGLEENCODERB</PropertyName>
        <Address>242</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>11</BitRangeStart>
        <BitRangeEnd>11</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TOGGLEENCODERB</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PRECHARGETHRESHOLD</PropertyName>
        <Address>243</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Pre Charge Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CONTACTORNOTCLOSEDTHRESHOLD</PropertyName>
        <Address>244</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Contactor Not Closed Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CONTACTORHIGHRESTHRESHOLD</PropertyName>
        <Address>245</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Contactor High Resistance Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAINSOLENOIDV</PropertyName>
        <Address>246</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>72</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>114</FlashOffset>

        <Name>Main Solenoid Voltage(36 or 48 or 72 or other)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ParkingBrakeVoltage</PropertyName>
        <Address>247</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Parking Brake Solenoid Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeLightRelayVoltage</PropertyName>
        <Address>248</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>116</FlashOffset>

        <Name>Brake Light Relay Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ADDRESS249</PropertyName>
        <Address>249</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ADDRESS249</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PARKINGBRAKETESTFAILENCODERCOUNTS</PropertyName>
        <Address>250</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>122</FlashOffset>

        <Name>Parking Brake Test Maximum Encoder Count Threshold</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ADDRESS251</PropertyName>
        <Address>251</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ADDRESS251</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SpeedPulseOutputDivider</PropertyName>
        <Address>252</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Speed Sensor Pulse Output Divider (0 disables)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>REGENRESISTORFAILVOLTAGE</PropertyName>
        <Address>253</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Regen Resistor Fail Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ADDRESS254</PropertyName>
        <Address>254</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ADDRESS254</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ADDRESS255</PropertyName>
        <Address>255</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ADDRESS255</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Development_1</PropertyName>
        <Address>256</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Development_1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Development_2</PropertyName>
        <Address>257</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Development_2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Development_3</PropertyName>
        <Address>258</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Development_3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Development_4</PropertyName>
        <Address>259</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Development_4</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RegenResistorOhms_q12</PropertyName>
        <Address>264</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Resistor Resistance (ohms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryType</PropertyName>
        <Address>265</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.8</ImplementedFirmwareVersion>
        <enumListName>
          <string>Invalid</string>
          <string>Lead Acid</string>
          <string>LiFePO4</string>
          <string>LiNiMnCoO2</string>
        </enumListName>
      <Name>Battery Cell Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>currentB_OffsetValue</PropertyName>
        <Address>266</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Sensor B, A2D Raw Offset Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>currentC_OffsetValue</PropertyName>
        <Address>267</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Sensor C, A2D Raw Offset Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>currentB_RawValue</PropertyName>
        <Address>268</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Sensor B, A2D Raw Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>currentC_RawValue</PropertyName>
        <Address>269</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Current Sensor C, A2D Raw Value</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeResistorOnVoltage_puq7</PropertyName>
        <Address>270</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>255</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>346</FlashOffset>

        <Name>Regen Resistor ON Voltage (p.u. of Rated Nominal System Voltage)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CtrlBrdHwRev</PropertyName>
        <Address>271</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Control Board Hardware Revision</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MAXCURRENT</PropertyName>
        <Address>272</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Maximum Phase Current (pu q12)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VelocityGainEndRPM</PropertyName>
        <Address>273</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity Gain End RPM</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>COASTINGTORQUERATE</PropertyName>
        <Address>274</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Coasting Torque Reduction Rate</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MINOFFTHROTTLEREGEN</PropertyName>
        <Address>275</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Minimum Off Throttle Regen Torque(PU Q12)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VelocityGainEndKp_q8</PropertyName>
        <Address>276</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity Gain End Kp_q8</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanBaudRateKHz</PropertyName>
        <Address>277</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanBaudRateKHz</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanHeartBeatData</PropertyName>
        <Address>278</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanHeartBeatData</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANJ1939Mfg1</PropertyName>
        <Address>279</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANJ1939Mfg1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANJ1939Mfg2</PropertyName>
        <Address>280</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANJ1939Mfg2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANJ1939Mfg3</PropertyName>
        <Address>281</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANJ1939Mfg3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANJ1939Mfg4</PropertyName>
        <Address>282</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANJ1939Mfg4</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANOpenDataNMT_State</PropertyName>
        <Address>283</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>6</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>4</string>
            <string>5</string>
            <string>127</string>
        </enumListValue>
        <enumListName>
            <string>BOOT</string>
            <string>STOP</string>
            <string>OPERATIONAL</string>
            <string>PRE-OPERATIONAL</string>
        </enumListName>

        <Name>CANOpenData NMT State</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VelocityGainEndKi_q8</PropertyName>
        <Address>284</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Velocity Gain End Ki_q8</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANOpenAlwayzDisplayPackedSocAndFaults</PropertyName>
        <Address>285</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANOpenAlwayzDisplayPackedSocAndFaults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMotorTempAndSwitch1</PropertyName>
        <Address>286</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMotorTempAndSwitch1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedHeartBeatAndMilePerHourLow</PropertyName>
        <Address>287</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedHeartBeatAndMilePerHourLow</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMilePerHourHigh</PropertyName>
        <Address>288</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMilePerHourHigh</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMileageLowAndChargerSwitch</PropertyName>
        <Address>289</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMileageLowAndChargerSwitch</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMileageHigh</PropertyName>
        <Address>290</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMileageHigh</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>AlwayzCANBmsVoltsAndSocAndFaultsAndStuff</PropertyName>
        <Address>291</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>AlwayzCANBmsVoltsAndSocAndFaultsAndStuff</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CANOpenAndGeneralStartupState</PropertyName>
        <Address>292</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANOpenAndGeneralStartupState</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TotalVehicleHours_6minPerBit</PropertyName>
        <Address>293</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1638.3</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TotalVehicleHours_6minPerBit</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939SPN248_TotalPowerTakeoffHours_3minPerBit</PropertyName>
        <Address>294</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939SPN248_TotalPowerTakeoffHours_3minPerBit</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939PGN_PackedTorqueSPNxxxAndRPMSPN190Low</PropertyName>
        <Address>295</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939PGN_PackedTorqueSPNxxxAndRPMSPN190Low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939PGN_PackedTorqueSPNxxxAndRPMSPN190High</PropertyName>
        <Address>296</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939PGN_PackedTorqueSPNxxxAndRPMSPN190High</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMotorCurrentLow</PropertyName>
        <Address>297</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMotorCurrentLow</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939IntimidatorDisplayPackedMotorCurrentHigh</PropertyName>
        <Address>298</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939IntimidatorDisplayPackedMotorCurrentHigh</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>J1939DeltaCustomKeyOn</PropertyName>
        <Address>299</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>J1939DeltaCustomKeyOn</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DischargeBatteryCurrentLimit_Aq4</PropertyName>
        <Address>300</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Low Voltage Battery Current Limiter</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>foldbackStartingLowVoltage_q7</PropertyName>
        <Address>301</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>foldback Starting Low Voltage </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>foldbackEndingLowVoltage_q7</PropertyName>
        <Address>302</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>foldback Ending Low Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MinimumBatteryCurrentFoldback_PUq12</PropertyName>
        <Address>303</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Minimum Battery Current Foldback</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PowerStageTestVectorBits</PropertyName>
        <Address>304</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Power Stage Test Vector</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VdcBusHalfBridgeTestResult_puq7</PropertyName>
        <Address>305</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vdc Bus Half Bridge Test Result</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PhaseAHalfBridgeTestResult_puq7</PropertyName>
        <Address>306</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PhaseA Half Bridge Test Result</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PhaseBHalfBridgeTestResult_puq7</PropertyName>
        <Address>307</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PhaseB Half Bridge Test Result</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PhaseCHalfBridgeTestResult_puq7</PropertyName>
        <Address>308</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PhaseC Half Bridge Test Result</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Background1msProcessingState</PropertyName>
        <Address>309</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Background 1ms Processing State</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Background10msProcessingState</PropertyName>
        <Address>310</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Background 10ms Processing State</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VectorIdqMagnitude_puq24</PropertyName>
        <Address>311</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VectorIdqMagnitude_puq24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VectorIdqAngle_q24</PropertyName>
        <Address>312</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VectorIdqAngle_q24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VectorVdqMagnitude_puq24</PropertyName>
        <Address>313</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VectorVdqMagnitude_puq24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VectorVdqAngle_q24</PropertyName>
        <Address>314</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VectorVdqAngle_q24</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Va</PropertyName>
        <Address>315</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Va</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Vb</PropertyName>
        <Address>316</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vb</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Vc</PropertyName>
        <Address>317</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vc</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>AngleNotEqualZero</PropertyName>
        <Address>318</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>AngleNotEqualZero</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VaOutInst_puq8</PropertyName>
        <Address>319</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VaOutInst_puq8</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VbOutInst_puq8</PropertyName>
        <Address>320</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VbOutInst_puq8</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VcOutInst_puq8</PropertyName>
        <Address>321</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>256</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>63.99609375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>VcOutInst_puq8</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>LowVoltageFoldbackStartingVoltage_q7</PropertyName>
        <Address>322</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>142</FlashOffset>

        <Name>Low voltage foldback To 0 battery current starting voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>LowVoltageFoldbackEndingVoltage_q7</PropertyName>
        <Address>323</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127.9921875</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Low voltage foldback To 0 battery current ending voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX10MilesOrKm_q0Low</PropertyName>
        <Address>324</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1638.3</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>OdometerX10MilesOrKm_q0Low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX10MilesOrKm_q0High</PropertyName>
        <Address>325</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1638.3</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>OdometerX10MilesOrKm_q0High</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>test5Settings</PropertyName>
        <Address>326</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>test5Settings</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>LowVoltageSpeedFoldback_PUq12</PropertyName>
        <Address>327</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>LowVoltageSpeedFoldback_PUq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MinSpeedFoldbackBeforeLowVoltage_PUq12</PropertyName>
        <Address>328</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>4</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>MinSpeedFoldbackBeforeLowVoltage_PUq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorThermistorNegative25CResistance</PropertyName>
        <Address>329</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>147</FlashOffset>

        <Name>MotorThermistorNegative25CResistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorThermistor25CResistance</PropertyName>
        <Address>330</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>148</FlashOffset>

        <Name>MotorThermistor25CResistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorThermistor75CResistance</PropertyName>
        <Address>331</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>149</FlashOffset>

        <Name>MotorThermistor75CResistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorThermistor125CResistance</PropertyName>
        <Address>332</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>150</FlashOffset>

        <Name>MotorThermistor125CResistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MotorThermistor175CResistance</PropertyName>
        <Address>333</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>151</FlashOffset>

        <Name>MotorThermistor175CResistance</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryCurrentAt5AToAmpHours</PropertyName>
        <Address>334</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryCurrentAt5AToAmpHours</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryCurrentAt25AToAmpHours</PropertyName>
        <Address>335</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryCurrentAt25AToAmpHours</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryCurrentAt55AToAmpHours</PropertyName>
        <Address>336</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryCurrentAt55AToAmpHours</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryCurrentAt105AToAmpHours</PropertyName>
        <Address>337</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryCurrentAt105AToAmpHours</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryCurrentAt200AToAmpHours</PropertyName>
        <Address>338</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryCurrentAt200AToAmpHours</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery0AFoldbackStartingLowVoltage_q7</PropertyName>
        <Address>339</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>157</FlashOffset>

        <Name>Battery0AFoldbackStartingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery25AFoldbackStartingLowVoltage_q7</PropertyName>
        <Address>340</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>158</FlashOffset>

        <Name>Battery25AFoldbackStartingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery75AFoldbackStartingLowVoltage_q7</PropertyName>
        <Address>341</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>159</FlashOffset>

        <Name>Battery75AFoldbackStartingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery150AFoldbackStartingLowVoltage_q7</PropertyName>
        <Address>342</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>160</FlashOffset>

        <Name>Battery150AFoldbackStartingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery225AFoldbackStartingLowVoltage_q7</PropertyName>
        <Address>343</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>161</FlashOffset>

        <Name>Battery225AFoldbackStartingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery0AFoldbackEndingLowVoltage_q7</PropertyName>
        <Address>344</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>162</FlashOffset>

        <Name>Battery0AFoldbackEndingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery25AFoldbackEndingLowVoltage_q7</PropertyName>
        <Address>345</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>163</FlashOffset>

        <Name>Battery25AFoldbackEndingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery75AFoldbackEndingLowVoltage_q7</PropertyName>
        <Address>346</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>164</FlashOffset>

        <Name>Battery75AFoldbackEndingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery150AFoldbackEndingLowVoltage_q7</PropertyName>
        <Address>347</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>165</FlashOffset>

        <Name>Battery150AFoldbackEndingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Battery225AFoldbackEndingLowVoltage_q7</PropertyName>
        <Address>348</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>256</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>166</FlashOffset>

        <Name>Battery225AFoldbackEndingLowVoltage_q7</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PresentEncoderAbsolutPosition</PropertyName>
        <Address>349</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Present Encoder Absolut Position</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>IntegralTrucationAvoidanceAndLowErrorIncrease</PropertyName>
        <Address>350</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>IntegralTrucationAvoidanceAndLowErrorIncrease</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>EncoderIndexFound</PropertyName>
        <Address>351</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>EncoderIndexFound</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Development_17</PropertyName>
        <Address>352</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Development_17</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ResolverEncoderSignalAMagnitude_Vq12</PropertyName>
        <Address>353</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ResolverEncoderSignalAMagnitude_Vq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ResolverEncoderSignalBMagnitude_Vq12</PropertyName>
        <Address>354</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ResolverEncoderSignalBMagnitude_Vq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ResolverEncoderSignalAOffset_Vq12</PropertyName>
        <Address>355</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ResolverEncoderSignalAOffset_Vq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ResolverEncoderSignalBOffset_Vq12</PropertyName>
        <Address>356</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>ResolverEncoderSignalBOffset_Vq12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gAdcData2Filtered_Vanaprog_Vq23_1</PropertyName>
        <Address>357</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Resolver Analog Signal A</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gAdcData2Filtered_Vanaprog_Vq23_2</PropertyName>
        <Address>358</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Resolver Analog Signal B</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RegenResistorPulseWidth</PropertyName>
        <Address>359</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>2</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Regen Resistor Short Circuit Pulse Width</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RegenResistorShortFaultThreshold</PropertyName>
        <Address>360</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>255</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Regen Resistor Short Circuit Voltage Drop Fault Level (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RunBrakeResistorShortCircuitTest</PropertyName>
        <Address>361</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Run Brake Resistor Short Circuit Test</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeResistorShortCircuitTest_EndCount</PropertyName>
        <Address>362</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Resistor Short Circuit Test Pulse Count</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryOverVoltageFoldbackStartingVoltage_puq7</PropertyName>
        <Address>363</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>255</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>173</FlashOffset>

        <Name>BatteryOverVoltageFoldbackStartingVoltage (p.u. of Rated Nominal System Voltage)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryOverVoltageFoldbackEndingVoltage_puq7</PropertyName>
        <Address>364</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>128</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>255</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>174</FlashOffset>

        <Name>BatteryOverVoltageFoldbackEndingVoltage (p.u. of Rated Nominal System Voltage)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryOverCurrentFoldbackStartingCurrent</PropertyName>
        <Address>365</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryOverCurrentFoldbackStartingCurrent</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryOverCurrentFoldbackEndingCurrent</PropertyName>
        <Address>366</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>BatteryOverCurrentFoldbackEndingCurrent</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FaultyEncoderMotorCurrentTrip_puq12</PropertyName>
        <Address>367</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>40.96</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0.0</MinimumParameterValue>
        <MaximumParameterValue>100.0</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Faulty Encoder Motor Current Fault Trip (% Rated Current. 0 to disable.)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryContinuousDischargeCurrent_Adc_q4</PropertyName>
        <Address>368</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>341</FlashOffset>

        <Name>Battery Continuous Discharge Current (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryDischargePulseCapacity</PropertyName>
        <Address>369</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16737</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>342</FlashOffset>

        <Name>Battery Discharge Pulse Capacity (Coulombs: Current(A) x Time(S))</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>veepromRegressionTest</PropertyName>
        <Address>370</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>V-EEPROM Regression (Bit0:Roll-over, Bit1:Invalid-Address, Bit2:Shifted-Data) </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ChargeIdc_Adc_q4_limit</PropertyName>
        <Address>371</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>43</FlashOffset>

        <Name>Battery Regen Current Limit (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryContinuousChargeCurrent_Adc_q4</PropertyName>
        <Address>372</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1023.9375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>343</FlashOffset>

        <Name>Battery Continuous Regen Current (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BatteryChargePulseCapacity</PropertyName>
        <Address>373</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>16737</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <FlashOffset>344</FlashOffset>

        <Name>Battery Regen Pulse Capacity (Coulombs: Current(A) x Time(S))</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>veepromRollOverCtr</PropertyName>
        <Address>374</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Virtual EEPROM Roll Over Count</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>eeWriteCtr</PropertyName>
        <Address>375</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Virtual EEPROM Writes</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RegenSlipFreqBoostStartFreq_pu_q12</PropertyName>
        <Address>376</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>8</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RegenSlipFreqBoostStartFreq_pu_q12</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>reserved49</PropertyName>
        <Address>377</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>4096</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>3.999755859375</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>reserved49</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>TimeToDisableMotorAfterStopping_ms</PropertyName>
        <Address>378</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TimeToDisableMotorAfterStopping_ms</Name>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gODDevelopmentProcTable0_index</PropertyName>
        <Address>379</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>gODDevelopmentProcTable0_index</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gODDevelopmentProcTable0_len</PropertyName>
        <Address>380</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>gODDevelopmentProcTable0_len</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gODDevelopmentProcTable0_SimpleModBusPointer</PropertyName>
        <Address>381</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>gODDevelopmentProcTable0_SimpleModBusPointer</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gODDevelopmentProcTable0_scale_q16_low</PropertyName>
        <Address>382</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>gODDevelopmentProcTable0_scale_q16_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gODDevelopmentProcTable0_scale_q16_high</PropertyName>
        <Address>383</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>gODDevelopmentProcTable0_scale_q16_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_15_ID_low</PropertyName>
        <Address>384</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Params.CanDataObjectConfig_15_ID_high</PropertyName>
        <Address>385</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_15_pdo_map_low</PropertyName>
        <Address>386</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_15_pdo_map_high</PropertyName>
        <Address>387</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_15_type</PropertyName>
        <Address>388</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_15_pdoTimer_event_time</PropertyName>
        <Address>389</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO6 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANESLow</PropertyName>
        <Address>390</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>5</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>64</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
            <string>2</string>
            <string>8</string>
            <string>16</string>
            <string>32</string>
        </enumListValue>
        <enumListName>
            <string>Idle</string>
            <string>Transmitting</string>
            <string>Receiving</string>
            <string>Power Down</string>
            <string>CCE</string>
            <string>Suspend Mode</string>
        </enumListName>

        <Name>CAN Error and Status (Low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANESHigh</PropertyName>
        <Address>391</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Error and Status (High 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_ID_low</PropertyName>
        <Address>392</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_ID_high</PropertyName>
        <Address>393</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_pdo_map_low</PropertyName>
        <Address>394</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_pdo_map_high</PropertyName>
        <Address>395</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_type</PropertyName>
        <Address>396</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_14_pdoTimer_event_time</PropertyName>
        <Address>397</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO5 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANTec</PropertyName>
        <Address>398</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Transmit Error Count</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANRec</PropertyName>
        <Address>399</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Receive Error Count</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_ID_low</PropertyName>
        <Address>400</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_CanOpenIds</PropertyName>
        <Address>400</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>7</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-16383</MinimumParameterValue>
        <MaximumParameterValue>16383</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <!--<string>0x000</string>  0
			<string>0x080</string>      128
			<string>0x200</string>      512
			<string>0x300</string>      768
			<string>0x180</string>      384
			<string>0x280</string>      640
			<string>0x380</string>      896
			<string>0x480</string>      1152
			<string>0x580</string>      1408
			<string>0x600</string>      1536
			<string>0x700</string>      1792 -->

            <string>0</string>
            <!--0,Rshft7 = 0-->
            <string>1</string>
            <!--128,Rshft7 = 1-->
            <string>4</string>
            <!--512,Rshft7 = 4-->
            <string>6</string>
            <!--768,Rshft7 = 6-->
            <string>3</string>
            <!--384,Rshft7 = 3-->
            <string>5</string>
            <!--640,Rshft7 = 5-->
            <string>7</string>
            <!--896,Rshft7 = 7-->
            <string>9</string>
            <!--1152,Rshft7 = 9-->
            <string>11</string>
            <!--1408,Rshft7 = 11-->
            <string>12</string>
            <!--1536,Rshft7 = 12-->
            <string>14</string>
            <!--1792,Rshft7 = 14-->

        </enumListValue>
        <!--#define NMT_MASTER_BASE 0x000
		#define SYNC_BASE		0x080
		#define EMCY_BASE		0x080
		#define RPDO1_BASE		0x200
		#define RPDO2_BASE		0x300
		#define TPDO1_BASE 		0x180
		#define TPDO2_BASE 		0x280
		#define TPDO3_BASE 		0x380
		#define TPDO4_BASE 		0x480
		#define TxSDO_BASE 		0x580
		#define RxSDO_BASE  	0x600
		#define TxSDO2_BASE 	0x6B1 //1280
		#define RxSDO2_BASE  	0x681  //1200
		#define HEARTBEAT_BASE 	0x700-->
        <enumListName>
            <string>NMT_MASTER_BASE</string>
            <string>SYNC_BASE_EMCY_BASE</string>
            <string>RPDO1_BASE</string>
            <string>RPDO2_BASE</string>
            <string>TPDO1_BASE</string>
            <string>TPDO2_BASE</string>
            <string>TPDO3_BASE</string>
            <string>TPDO4_BASE</string>
            <string>TxSDO_BASE</string>
            <string>RxSDO_BASE</string>
            <string>HEARTBEAT_BASE</string>
        </enumListName>
        <Name>CanDataObjectConfig_13_CanOpenIds</Name>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_ID_high</PropertyName>
        <Address>401</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_pdo_map_low</PropertyName>
        <Address>402</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_pdo_map_high</PropertyName>
        <Address>403</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_type</PropertyName>
        <Address>404</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_13_pdoTimer_event_time</PropertyName>
        <Address>405</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO4 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanNetworkControlMode</PropertyName>
        <Address>406</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Can Network Control Mode (0=Stand Alone, 1=Leader, 2=Follower)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ShutDownFaultHasOccured</PropertyName>
        <Address>407</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Shut Down Fault Has Occured</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_ID_low</PropertyName>
        <Address>408</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_ID_high</PropertyName>
        <Address>409</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_pdo_map_low</PropertyName>
        <Address>410</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_pdo_map_high</PropertyName>
        <Address>411</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_type</PropertyName>
        <Address>412</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_pdoTimer_event_time</PropertyName>
        <Address>413</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO3 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>MaximumWheelSpeedDiff_MPHX10</PropertyName>
        <Address>414</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Maximum Networked Wheel Speed Difference (MPHX10)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_12_spare2</PropertyName>
        <Address>415</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Network Data Source.  If Bit# = 1 => Use value from CAN instead of local value.</Name>
        <Description>
            <![CDATA[
            <b>Description:</b> Bit0: SOC, Bit2: Battery Current, Bit3: Discharge Current Limit, Bit4: Regen Current Limit, Bit5: Throttle, Bit6: Digital Inputs
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>BMS_BATTERY_SOC</PropertyName>
        <Address>415</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Network Data SOC.</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BMS_BATTERY_VOLTAGE</PropertyName>
        <Address>415</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Network Data Voltage</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BMS_BATTERY_CURRENT</PropertyName>
        <Address>415</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Network Data Current</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_ID_low</PropertyName>
        <Address>416</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_ID_high</PropertyName>
        <Address>417</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_pdo_map_low</PropertyName>
        <Address>418</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_pdo_map_high</PropertyName>
        <Address>419</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_type</PropertyName>
        <Address>420</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_11_pdoTimer_event_time</PropertyName>
        <Address>421</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO2 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANTecAccum</PropertyName>
        <Address>422</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANTec Accumulator</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gCANRecAccum</PropertyName>
        <Address>423</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CANRec Accumulator</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_ID_low</PropertyName>
        <Address>424</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_ID_high</PropertyName>
        <Address>425</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_pdo_map_low</PropertyName>
        <Address>426</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_pdo_map_high</PropertyName>
        <Address>427</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_type</PropertyName>
        <Address>428</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_pdoTimer_event_time</PropertyName>
        <Address>429</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>TPDO1 Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Params.CanDataObjectConfig_10_spare1</PropertyName>
        <Address>430</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Params.CanDataObjectConfig_10_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_10_spare2</PropertyName>
        <Address>431</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_10_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_ID_low</PropertyName>
        <Address>432</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_ID_high</PropertyName>
        <Address>433</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_pdo_map_low</PropertyName>
        <Address>434</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_pdo_map_high</PropertyName>
        <Address>435</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_type</PropertyName>
        <Address>436</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_pdoTimer_event_time</PropertyName>
        <Address>437</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_spare1</PropertyName>
        <Address>438</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_9_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_9_spare2</PropertyName>
        <Address>439</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_9_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_ID_low</PropertyName>
        <Address>440</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_ID_high</PropertyName>
        <Address>441</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_pdo_map_low</PropertyName>
        <Address>442</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_pdo_map_high</PropertyName>
        <Address>443</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_type</PropertyName>
        <Address>444</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_pdoTimer_event_time</PropertyName>
        <Address>445</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_spare1</PropertyName>
        <Address>446</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_8_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_8_spare2</PropertyName>
        <Address>447</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_8_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_ID_low</PropertyName>
        <Address>448</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_ID_high</PropertyName>
        <Address>449</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_pdo_map_low</PropertyName>
        <Address>450</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_pdo_map_high</PropertyName>
        <Address>451</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_type</PropertyName>
        <Address>452</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_pdoTimer_event_time</PropertyName>
        <Address>453</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Params.CanDataObjectConfig_7_spare1</PropertyName>
        <Address>454</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Params.CanDataObjectConfig_7_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_7_spare2</PropertyName>
        <Address>455</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_7_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_ID_low</PropertyName>
        <Address>456</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_ID_high</PropertyName>
        <Address>457</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_pdo_map_low</PropertyName>
        <Address>458</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_pdo_map_high</PropertyName>
        <Address>459</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_type</PropertyName>
        <Address>460</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_pdoTimer_event_time</PropertyName>
        <Address>461</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>HeartBeat Event Time (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_spare1</PropertyName>
        <Address>462</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_6_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_6_spare2</PropertyName>
        <Address>463</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_6_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_ID_low</PropertyName>
        <Address>464</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_ID_high</PropertyName>
        <Address>465</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_pdo_map_low</PropertyName>
        <Address>466</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_pdo_map_high</PropertyName>
        <Address>467</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_type</PropertyName>
        <Address>468</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_pdoTimer_event_time</PropertyName>
        <Address>469</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_spare1</PropertyName>
        <Address>470</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_5_spare2</PropertyName>
        <Address>471</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_5_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_ID_low</PropertyName>
        <Address>472</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_ID_high</PropertyName>
        <Address>473</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_pdo_map_low</PropertyName>
        <Address>474</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_pdo_map_high</PropertyName>
        <Address>475</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_type</PropertyName>
        <Address>476</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_pdoTimer_event_time</PropertyName>
        <Address>477</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_spare1</PropertyName>
        <Address>478</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_4_spare2</PropertyName>
        <Address>479</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_4_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_ID_low</PropertyName>
        <Address>480</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_ID_high</PropertyName>
        <Address>481</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_pdo_map_low</PropertyName>
        <Address>482</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_pdo_map_high</PropertyName>
        <Address>483</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_type</PropertyName>
        <Address>484</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_pdoTimer_event_time</PropertyName>
        <Address>485</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_spare1</PropertyName>
        <Address>486</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_3_spare2</PropertyName>
        <Address>487</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_3_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_ID_low</PropertyName>
        <Address>488</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_ID_high</PropertyName>
        <Address>489</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_pdo_map_low</PropertyName>
        <Address>490</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_pdo_map_high</PropertyName>
        <Address>491</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_type</PropertyName>
        <Address>492</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_pdoTimer_event_time</PropertyName>
        <Address>493</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_spare1</PropertyName>
        <Address>494</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_2_spare2</PropertyName>
        <Address>495</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_2_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_ID_low</PropertyName>
        <Address>496</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_ID_high</PropertyName>
        <Address>497</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_pdo_map_low</PropertyName>
        <Address>498</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_pdo_map_high</PropertyName>
        <Address>499</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_type</PropertyName>
        <Address>500</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_pdoTimer_event_time</PropertyName>
        <Address>501</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_spare1</PropertyName>
        <Address>502</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_1_spare2</PropertyName>
        <Address>503</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_1_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_ID_low</PropertyName>
        <Address>504</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_ID_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_ID_high</PropertyName>
        <Address>505</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_ID_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_pdo_map_low</PropertyName>
        <Address>506</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_pdo_map_low</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_pdo_map_high</PropertyName>
        <Address>507</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_pdo_map_high</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_type</PropertyName>
        <Address>508</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_pdoTimer_event_time</PropertyName>
        <Address>509</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_pdoTimer_event_time</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_spare1</PropertyName>
        <Address>510</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_spare1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig_0_spare2</PropertyName>
        <Address>511</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CanDataObjectConfig_0_spare2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DEBUGVAR1_TEST_NEW_PARAM_PAGE</PropertyName>
        <Address>512</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <RootEnumList>
            <RootEnumItem>
                <NestedEnumListName>
                    <string>Run</string>
                    <string>Tow</string>
                </NestedEnumListName>
                <name>Run/Tow Input</name>
                <description>
                    <![CDATA[
              Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A,RXV_ELITE_LITHIUM_600A)
            <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png)(ImageOverlays:_key_run_charge.png)
             The run/tow switch when switched to tow, connects the battery voltage to this input. When switched to tow the controller will disengage the parking brake when the key is turned from off to on.
            <br><b>•</b> If for some reason this switch does not work, there are jumpers under the switch to force the brake to be disengaged without the controller being powered.
            <br>
            <br><b>Tow:</b> battery voltage
            <br><b>Run:</b> no connection, pulled to 0V internally
            <br>Troubleshooting Notes:
            <br><b>1.</b> If this pin is not connected, the App indicates the vehicle is in Tow and this signal is preventing the vehicle from driving.
            <br><b>2.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch key off)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the run/tow is switched to tow and 0V when switched to run??????.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the run/tow  switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
            <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png)(ImageOverlays:_key_run_charge.png)
             On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
            <br><b>•</b> the controller B+ post provides high power to the motor, B+ is not directly connected internally to this input.
            <br><b>•</b> This input will precharge internal high power circuits with limited current before allowing the Main solenoid to close.
            <br>
            <br><b>Run:</b> battery voltage
            <br><b>Tow:</b> no connection, no power
            <br>Troubleshooting Notes:
            <br><b>1.</b> If this pin is not connected to the battery then the controller will not show up in the Bluetooth list. The App will always show Run
            <br><b>2.</b> Verify the red led on the controller flashes on for 1 second and then turns off when the key is turned on.
            <br><b>3.</b> No led flash at power up may mean something is wrong with the vehicle harness.
            <br><b>4.</b> If there are any diagnostic errors the red led will flash a code which can be decoded with this App.
            <br><b>5.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to Tow)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the run/tow is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.
            
            Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
            <b>Description:</b>(Images:TAC2_Yamaha_G29.png)(ImageOverlays:_key_run_charge.png)
             On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
            <br><b>•</b> the controller B+ post provides high power to the motor, B+ is not directly connected internally to this input.
            <br><b>•</b> This input will precharge internal high power circuits with limited current before allowing the Main solenoid to close.
            <br>
            <br><b>Run:</b> battery voltage
            <br><b>Tow:</b> no connection, no power
            <br>Troubleshooting Notes:
            <br><b>1.</b> If this pin is not connected to the battery then the controller will not show up in the Bluetooth list. The App will always show Run
            <br><b>2.</b> Verify the red led on the controller flashes on for 1 second and then turns off when the key is turned on.
            <br><b>3.</b> No led flash at power up may mean something is wrong with the vehicle harness.
            <br><b>4.</b> If there are any diagnostic errors the red led will flash a code which can be decoded with this App.
            <br><b>5.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to Tow)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the run/tow is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.

            Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
            <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png)(ImageOverlays:_key_run_charge.png) On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
            <br><b>•</b> the controller B+ post provides high power to the motor, B+ is not directly connected internally to this input.
            <br><b>•</b> This input will precharge internal high power circuits with limited current before allowing the Main solenoid to close.
            <br>
            <br><b>Run:</b> battery voltage
            <br><b>Tow:</b> no connection, no power
            <br>Troubleshooting Notes:
            <br><b>1.</b> If this pin is not connected to the battery then the controller will not show up in the Bluetooth list. The App will always show Run
            <br><b>2.</b> Verify the red led on the controller flashes on for 1 second and then turns off when the key is turned on.
            <br><b>3.</b> No led flash at power up may mean something is wrong with the vehicle harness.
            <br><b>4.</b> If there are any diagnostic errors the red led will flash a code which can be decoded with this App.
            <br><b>5.</b> Checking the vehicle harness:
            <br><b>a)</b> REMEBER TO TURN VEHICLE POWER OFF before connecting or disconnecting the harness (switch to Tow)
            <br><b>b)</b> Disconnect the vehicle harness from the controller.
            <br><b>c)</b> With one side of a meter connected to the battery pack ground, measure that this pin on the vehicle harness has battery voltage when the run/tow is switched to on and 0V when switched to off.
            <br><b>d)</b> if c) fails then verify with a meter that the other side of the key switch has battery voltage.
        ]]>
                </description>
            </RootEnumItem>
            <RootEnumItem>
                <NestedEnumListName>
                    <string>Turtle</string>
                    <string>Rabbit</string>
                </NestedEnumListName>
                <name>Rabbit/Turtle Input</name>
                <description>
                    <![CDATA[
        ]]>
                </description>
            </RootEnumItem>
        </RootEnumList>

        <Name>Run/Tow Input Options (0 = Run/Tow, 1 = Rabbit/Turtle)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>InputAnalogBrakeOptions</PropertyName>
        <Address>513</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
        </enumListValue>
        <enumListName>
            <string>Analog Brake Input</string>
            <string>Battery Charger Signal</string>
        </enumListName>

        <Name>Analog Brake Input Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO1_Word2</PropertyName>
        <Address>514</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 - Data Word 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO1_Word3</PropertyName>
        <Address>515</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 - Data Word 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO1_Word4</PropertyName>
        <Address>516</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 - Data Word 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO1_timeout_status</PropertyName>
        <Address>517</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 - Timeout Status</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO2_Word1</PropertyName>
        <Address>518</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 - Data Word 0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO2_Word2</PropertyName>
        <Address>519</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 - Data Word 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO2_Word3</PropertyName>
        <Address>520</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 - Data Word 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO2_Word4</PropertyName>
        <Address>521</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 - Data Word 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO2_timeout_status</PropertyName>
        <Address>522</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO2 - Timeout Status</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO1_Word1</PropertyName>
        <Address>523</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO1 - Data Word 0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>VehicleLogicBrakingAndShutDownFaultHasOccured</PropertyName>
        <Address>524</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vehicle Logic Braking, and ShutDown Fault Has Occured</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>display_SwitchesAndGear</PropertyName>
        <Address>525</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Display: Switches And Gear Selection</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryVoltage_X10V</PropertyName>
        <Address>526</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Voltage from BMS (0.1V resolution)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatterySOC</PropertyName>
        <Address>527</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery SOC from BMS (%)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryCurrent_AX10_q0</PropertyName>
        <Address>528</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Current from BMS (0.1A resolution)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryDischargeCurrentLimit_AX10_q0</PropertyName>
        <Address>529</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Discharge Current Limit from BMS (0.1A resolution)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryChargeCurrentLimit_AX10_q0</PropertyName>
        <Address>530</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Charge(regen) Current Limit from BMS (0.1A resolution)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryAlarmCode</PropertyName>
        <Address>531</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Alarm Code from BMS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryWarningCode</PropertyName>
        <Address>532</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Battery Warning Code from BMS</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_ID_low</PropertyName>
        <Address>533</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_ID_high</PropertyName>
        <Address>534</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_pdo_map_low</PropertyName>
        <Address>535</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_pdo_map_high</PropertyName>
        <Address>536</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_type</PropertyName>
        <Address>537</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_2_pdoTimer_event_time</PropertyName>
        <Address>538</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_ID_low</PropertyName>
        <Address>539</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_ID_high</PropertyName>
        <Address>540</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_pdo_map_low</PropertyName>
        <Address>541</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_pdo_map_high</PropertyName>
        <Address>542</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_type</PropertyName>
        <Address>543</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_1_pdoTimer_event_time</PropertyName>
        <Address>544</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_ID_low</PropertyName>
        <Address>545</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_ID_high</PropertyName>
        <Address>546</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_pdo_map_low</PropertyName>
        <Address>547</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_pdo_map_high</PropertyName>
        <Address>548</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_type</PropertyName>
        <Address>549</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig2_0_pdoTimer_event_time</PropertyName>
        <Address>550</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>remote_BatteryCurrent_Adc_q4</PropertyName>
        <Address>551</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Dual Drive Remote_Battery Current (A)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>gRxRPDO3.MBoxUnion.words.WORD0</PropertyName>
        <Address>552</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 - Data Word 0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO3.MBoxUnion.words.WORD1</PropertyName>
        <Address>553</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 - Data Word 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO3.MBoxUnion.words.WORD2</PropertyName>
        <Address>554</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 - Data Word 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO3.MBoxUnion.words.WORD3</PropertyName>
        <Address>555</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 - Data Word 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO3_timeout_status</PropertyName>
        <Address>556</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO3 - Timeout Status</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>OutputReverseBuzzerOptions</PropertyName>
        <Address>557</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>7</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
            <string>2</string>
            <string>3</string>
            <string>4</string>
            <string>5</string>
            <string>6</string>
            <string>7</string>
        </enumListValue>
        <enumListName>
            <string>Reverse Buzzer</string>
            <string>Parking Brake Release</string>
            <string>Disable Reverse Buzzer</string>
            <string>Invalid</string>
            <string>Enable Forward Buzzer</string>
            <string>Invalid</string>
            <string>Disable Reverse Buzzer and Enable Forward Buzzer</string>
            <string>Invalid</string>
        </enumListName>

        <Name>Reverse Buzzer Output Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OutputBrakeLightRelayOptions</PropertyName>
        <Address>558</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
        </enumListValue>
        <enumListName>
            <string>Brake Light Relay</string>
            <string>External Enable Output (Toggles with Key)</string>
        </enumListName>

        <Name>Brake Light Relay Output Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>InputKeyOptions</PropertyName>
        <Address>559</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
        </enumListValue>
        <enumListName>
            <string>Key Switch</string>
            <string>Seat Switch</string>
        </enumListName>

        <Name>Key Switch Input Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OutputBrakeReleaseOptions</PropertyName>
        <Address>560</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <enumListValue>
            <string>0</string>
            <string>1</string>
        </enumListValue>
        <enumListName>
            <string>Brake Release Output</string>
            <string>Speed Pulser Output</string>
        </enumListName>

        <Name>Brake Release Output Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PackedOdomHighAndSoc</PropertyName>
        <Address>561</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PackedOdomHighAndSoc</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>vehicleSpeedx10KPH_q0</PropertyName>
        <Address>562</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Vehicle Speed (KPHx10)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX10Km_q0</PropertyName>
        <Address>563</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Odometer Low Word (Km x10)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdometerX10Km_q0+1</PropertyName>
        <Address>564</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Odometer High Byte (Km x10)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PackedDirectionSpeedModeFault</PropertyName>
        <Address>565</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PackedDirectionSpeedModeFault</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>DisplayOptions</PropertyName>
        <Address>566</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Display Options</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>canDisplayFaultCode</PropertyName>
        <Address>567</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>CAN Display Fault Code</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PackedSwVerOdomLow</PropertyName>
        <Address>568</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>PackedSwVerOdomLow</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>OdomHighX10Km_q0</PropertyName>
        <Address>569</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>OdomHighX10Km_q0 (Byte1 and Byte2 of three byte value)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>Otp_PhaseACurrSenseGain</PropertyName>
        <Address>570</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Phase A Current Sensor Gain (OTP)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>Otp_PhaseCCurrSenseGain</PropertyName>
        <Address>571</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Phase C Current Sensor Gain (OTP)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>gRxRPDO4.MBoxUnion.words.WORD0</PropertyName>
        <Address>572</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 - Data Word 0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>gRxRPDO4.MBoxUnion.words.WORD1</PropertyName>
        <Address>573</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 - Data Word 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO4.MBoxUnion.words.WORD2</PropertyName>
        <Address>574</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 - Data Word 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO4.MBoxUnion.words.WORD3</PropertyName>
        <Address>575</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 - Data Word 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO4_timeout_status</PropertyName>
        <Address>576</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO4 - Timeout Status</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>gRxRPDO5.MBoxUnion.words.WORD0</PropertyName>
        <Address>577</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 - Data Word 0</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>gRxRPDO5.MBoxUnion.words.WORD1</PropertyName>
        <Address>578</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 - Data Word 1</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO5.MBoxUnion.words.WORD2</PropertyName>
        <Address>579</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 - Data Word 2</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRxRPDO5.MBoxUnion.words.WORD3</PropertyName>
        <Address>580</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 - Data Word 3</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>gRPDO5_timeout_status</PropertyName>
        <Address>581</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>RPDO5 - Timeout Status</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>RoyalEv72VDisplayIoAndFaults</PropertyName>
        <Address>582</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Royal-EV 72V Display I/O And Faults</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RoyalEv72VMotorRpmAndVehicleSpeed</PropertyName>
        <Address>583</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Royal-EV 72V Motor RPM And Vehicle Speed</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>RoyalEv72VMode</PropertyName>
        <Address>584</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Royal-EV 72V Mode (1=ECO, 2=Sport, 4=Tow)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>InputOtfKeyOptions</PropertyName>
        <Address>585</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>7.100</ImplementedFirmwareVersion>

        <RootEnumList>
            <RootEnumItem>
                <NestedEnumListName>
                    <string>Locked</string>
                    <string>Unlocked</string>
                </NestedEnumListName>
                <name>OTF key switch</name>
                <description>
                    <![CDATA[
            <b>Description:</b> If this input is unlocked the voltages on the three other inputs determine the vehicle settings
            
              ]]>
                </description>
            </RootEnumItem>
            <RootEnumItem>
                <NestedEnumListName>
                    <string>Turtle</string>
                    <string>Rabbit</string>
                </NestedEnumListName>
                <name>Rabbit/Turtle Mode Select</name>
                <description>
                    <![CDATA[
                ]]>
                </description>
            </RootEnumItem>
        </RootEnumList>

        <Name>OTF Key Input Options.  0=OTF Key, 1=Rabbit/Turtle Mode Select</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>FaultOverRideMask</PropertyName>
        <Address>586</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Fault Over-Ride Mask.  Bit0:Park Brake.  Bit1:Charger Interlock.  Bit2:Startup Precharge. </Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>BrakeCheckFaultMask</PropertyName>
        <Address>586</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Brake Check Fault Mask</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>ChargerFaultMask</PropertyName>
        <Address>586</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>1</BitRangeStart>
        <BitRangeEnd>1</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Charger Fault Mask</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>PrechargeFaultMask</PropertyName>
        <Address>586</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>2</BitRangeStart>
        <BitRangeEnd>2</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>Pre Charge Fault Mask</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_StateOfCharge_percent</PropertyName>
        <Address>587</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

        <Name>State of Charge from BMS (%)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>InputEncoderCOptions</PropertyName>
        <Address>588</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>65535</MaximumParameterValue>
        <ImplementedFirmwareVersion>7.900</ImplementedFirmwareVersion>

        <Name>Speed-C Input Options.  0 = Encoder C.  1 = Rabbit/Turtle Select</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>SPEEDCRABBITTURTLE</PropertyName>
        <Address>588</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>0</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>7.900</ImplementedFirmwareVersion>

        <RootEnumList>
            <RootEnumItem>
                <NestedEnumListValue>
                    <string>0</string>
                    <string>1</string>
                    <string>2</string>
                    <string>3</string>
                    <string>4</string>
                    <string>5</string>
                    <string>6</string>
                    <string>7</string>
                </NestedEnumListValue>
                <NestedEnumListName>
                    <string>Changing  (Low)</string>
                    <string>Changing (High)</string>
                    <string>Not Changing  (Low)</string>
                    <string>Not Changing (High)</string>
                    <string>Fault  (Low)</string>
                    <string>Fault (High)</string>
                    <string>Fault  (Low)</string>
                    <string>Fault (High)</string>
                </NestedEnumListName>
                <name>Encoder C</name>
            </RootEnumItem>
            <RootEnumItem>
                <NestedEnumListName>
                    <string>Turtle</string>
                    <string>Rabbit</string>
                </NestedEnumListName>
                <name>Rabbit/Turtle Select</name>
            </RootEnumItem>
        </RootEnumList>

        <Name>Speed-C Input as Rabbit/Turtle Select</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>MAXMFGFWDSPEED</PropertyName>
        <Address>589</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>LoadableOTP</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.1</ImplementedFirmwareVersion>

        <Name>Vechcle Manufactures Max Forward Speed</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
        <PropertyName>NEWPROFILENUMBER</PropertyName>
        <Address>590</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>99.0</ImplementedFirmwareVersion>

        <Name>NewProfileNumber</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>

    </GoiParameter>

    <GoiParameter>
        <PropertyName>PARAMETERLENGTH</PropertyName>
        <Address>591</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.1</ImplementedFirmwareVersion>

        <Name>Parameter Length</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>

    </GoiParameter>

    <GoiParameter>
        <PropertyName>MAXMFGREVSPEED</PropertyName>
        <Address>592</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>LoadableOTP</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>127</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.1</ImplementedFirmwareVersion>

        <Name>Vechcle Manufactures Max Reverse Speed</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
	<GoiParameter>
		<PropertyName>can_Otf_prog1</PropertyName>
		<Address>593</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>CAN OTF Speed Limit Raw Value</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>

	</GoiParameter>

	<GoiParameter>
		<PropertyName>can_Otf_prog2</PropertyName>
		<Address>594</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>CAN OTF Regen Limit Raw Value</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>

	</GoiParameter>

	<GoiParameter>
		<PropertyName>can_Otf_prog3</PropertyName>
		<Address>595</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>CAN OTF Accel Limit Raw Value</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>

	</GoiParameter>

	<GoiParameter>
		<PropertyName>can_Otf_key</PropertyName>
		<Address>596</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>CAN OTF Key (0:Locked, 1:Unlocked)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>

	</GoiParameter>
  <GoiParameter>
    <PropertyName>BatteryConfigurationProfileNumber</PropertyName>
    <Address>597</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>Flash</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32767</MaximumParameterValue>
    <ImplementedFirmwareVersion>8.9</ImplementedFirmwareVersion>
    <Name>Battery Model Identifier</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
    <GoiParameter>
        <PropertyName>ID_WEAKENING_GAIN</PropertyName>
        <Address>598</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32000</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.5</ImplementedFirmwareVersion>

        <Name>Field weakening gain</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

	<GoiParameter>
		<PropertyName>RpdoEndianessMap</PropertyName>
		<Address>599</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO Endianess Map.  Bit0: RPDO1 ... Bit5: RPDO6. 1=Big, 0=Little.</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>

	<GoiParameter>
    <PropertyName>VectorVdqMagnitudeActual_puq24</PropertyName>
    <Address>600</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>256</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorVdqMagnitudeActual_puq24</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorVdqMagnitudeActualFiltered_puq24hi</PropertyName>
    <Address>601</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorVdqMagnitudeActualFiltered_puq24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorVdqMagnitudeFiltered_puq24hi</PropertyName>
    <Address>602</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorVdqMagnitudeFiltered_puq24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorIdqMagnitudeFiltered_puq24hi</PropertyName>
    <Address>603</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorIdqMagnitudeFiltered_puq24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorVdqAngleFiltered_q24hi</PropertyName>
    <Address>604</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorVdqAngleFiltered_q24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorIdqAngleFiltered_q24hi</PropertyName>
    <Address>605</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorIdqAngleFiltered_q24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>zMagnitudehi</PropertyName>
    <Address>606</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>zMagnitudehi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>zPhasehi</PropertyName>
    <Address>607</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>zPhasehi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>R_OHMq16hi</PropertyName>
    <Address>608</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>R(all)(Ohms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>R_OHMq16</PropertyName>
    <Address>609</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>R(all)(mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Rstator_OHMq16hi</PropertyName>
    <Address>610</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Rstator(Ohms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Rstator_OHMq16</PropertyName>
    <Address>611</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>65535</MaximumParameterValue>

    <Name>Rstator(mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Rrotor_OHMq16hi</PropertyName>
    <Address>612</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Rrotor(Ohms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Rrotor_OHMq16</PropertyName>
    <Address>613</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>65535</MaximumParameterValue>

    <Name>Rrotor(mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>L_Hq24hi</PropertyName>
    <Address>614</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>0.256</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>L(mH)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>L_Hq24</PropertyName>
    <Address>615</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>16.777216</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>65535</MaximumParameterValue>

    <Name>L(uH)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>zLowFreqMagnitudehi</PropertyName>
    <Address>616</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>zLowFreqMagnitudehi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>RstatorLowFreq_OHMq16</PropertyName>
    <Address>617</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>65535</MaximumParameterValue>

    <Name>Rstator Filtered in (mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>RLowFreq_OHMq16hi</PropertyName>
    <Address>618</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>RLowFreq_OHMq24hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>RLowFreq_OHMq16</PropertyName>
    <Address>619</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>R(all low Freq)(mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Trotor_Sq16</PropertyName>
    <Address>620</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Rotor time constant (s)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>TrotorInverseHi</PropertyName>
    <Address>621</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Inverse Tr Hi</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>TrotorInverseLo</PropertyName>
    <Address>622</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Inverse Tr Lo</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>FREQLo</PropertyName>
    <Address>623</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Fraction of Open loop Freq (Hz)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>VectorVdqAngleActual_q24</PropertyName>
    <Address>624</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>256</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>VectorVdqAngleActual_q24</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>RrotorLowFreq_OHM</PropertyName>
    <Address>625</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>RLowFreqRotor(Ohms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>RrotorLowFreq_OHMq16</PropertyName>
    <Address>626</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>Rrotor Filtered in (mOhms)</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>TestSlotTimer</PropertyName>
    <Address>627</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>65.536</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>32000</MaximumParameterValue>

    <Name>TestSlotTimer</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
    
	<GoiParameter>
		<PropertyName>PackedCanOtfKeyAndPowerX10V</PropertyName>
		<Address>628</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>65535</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>CAN OTF Key and OTF Power Supply Voltage (LSB: OTF Key, MSB: OTF PS Voltage (x10Volts)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>

	<GoiParameter>
        <PropertyName>OTFCANKEYINPUT</PropertyName>
        <Address>628</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>8</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>1</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

        <Name>CAN OTF Key Input</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
	<GoiParameter>
        <PropertyName>OTFCANPOWERSUPPLY</PropertyName>
        <Address>628</Address>
        <SubsetOfAddress>true</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>7</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>255</MaximumParameterValue>
        <ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

        <Name>CAN OTF Power Supply (V)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_ID_low</PropertyName>
		<Address>629</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 COB-ID (11-bit)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_ID_high</PropertyName>
		<Address>630</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 COB-ID (18-bit)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_pdo_map_low</PropertyName>
		<Address>631</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 PDO Map (low 16-bits)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_pdo_map_high</PropertyName>
		<Address>632</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 PDO Map (high 16-bits)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_type</PropertyName>
		<Address>633</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 Type</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>CanDataObjectConfig3_0_pdoTimer_event_time</PropertyName>
		<Address>634</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>Flash</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 Timeout (ms)</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>

	<GoiParameter>
		<PropertyName>gRxRPDO6.MBoxUnion.words.WORD0</PropertyName>
		<Address>635</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>65535</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 - Data Word 0</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>

	<GoiParameter>
		<PropertyName>gRxRPDO6.MBoxUnion.words.WORD1</PropertyName>
		<Address>636</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>65535</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 - Data Word 1</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>gRxRPDO6.MBoxUnion.words.WORD2</PropertyName>
		<Address>637</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>65535</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 - Data Word 2</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>gRxRPDO6.MBoxUnion.words.WORD3</PropertyName>
		<Address>638</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>0</MinimumParameterValue>
		<MaximumParameterValue>65535</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 - Data Word 3</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
	<GoiParameter>
		<PropertyName>gRPDO6_timeout_status</PropertyName>
		<Address>639</Address>
		<SubsetOfAddress>false</SubsetOfAddress>
		<Scale>1</Scale>
		<MemoryType>ReadOrWrite</MemoryType>
		<BitRangeStart>0</BitRangeStart>
		<BitRangeEnd>15</BitRangeEnd>
		<MinimumParameterValue>-32768</MinimumParameterValue>
		<MaximumParameterValue>32767</MaximumParameterValue>
		<ImplementedFirmwareVersion>8.6</ImplementedFirmwareVersion>

		<Name>RPDO6 - Timeout Status</Name>
		<Description>
			<![CDATA[
        ]]>
		</Description>
	</GoiParameter>
    <GoiParameter>
        <PropertyName>Otp_dcbusScale</PropertyName>
        <Address>640</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16384</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>9.0</ImplementedFirmwareVersion>

        <Name>OTP DCBus Scale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>

    </GoiParameter>

    <GoiParameter>
        <PropertyName>Otp_logicPowerScale</PropertyName>
        <Address>641</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>16384</Scale>
        <MemoryType>ReadOrWrite</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>9.0</ImplementedFirmwareVersion>

        <Name>OTP Logic Scale</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>

    </GoiParameter>

    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_ID_low</PropertyName>
        <Address>647</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 COB-ID (11-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_ID_high</PropertyName>
        <Address>648</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 COB-ID (18-bit)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_pdo_map_low</PropertyName>
        <Address>649</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 PDO Map (low 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_pdo_map_high</PropertyName>
        <Address>650</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 PDO Map (high 16-bits)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_type</PropertyName>
        <Address>651</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 Type</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>CanDataObjectConfig3_1_pdoTimer_event_time</PropertyName>
        <Address>652</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>TPDO6 Timeout (ms)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
    <GoiParameter>
        <PropertyName>bms_BatteryCurrent_AX10_q0_pos</PropertyName>
        <Address>653</Address>
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>10</Scale>
        <MemoryType>Flash</MemoryType>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>-32768</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>10.5</ImplementedFirmwareVersion>

        <Name>Battery Current from BMS (0.1A resolution)</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>

    <GoiParameter>
    <PropertyName>Development_5</PropertyName>
    <Address>756</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_5</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_6</PropertyName>
    <Address>757</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_6</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_7</PropertyName>
    <Address>758</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_7</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_8</PropertyName>
    <Address>759</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_8</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_9</PropertyName>
    <Address>760</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>256</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_9</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_10</PropertyName>
    <Address>761</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>256</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_10</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_11</PropertyName>
    <Address>762</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_11</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_12</PropertyName>
    <Address>763</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_12</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_13</PropertyName>
    <Address>764</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_13</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_14</PropertyName>
    <Address>765</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_14</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_15</PropertyName>
    <Address>766</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_15</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>
  <GoiParameter>
    <PropertyName>Development_16</PropertyName>
    <Address>767</Address>
    <SubsetOfAddress>false</SubsetOfAddress>
    <Scale>1</Scale>
    <MemoryType>ReadOrWrite</MemoryType>
    <BitRangeStart>0</BitRangeStart>
    <BitRangeEnd>15</BitRangeEnd>
    <MinimumParameterValue>0</MinimumParameterValue>
    <MaximumParameterValue>16383</MaximumParameterValue>
    <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>

    <Name>Development_16</Name>
    <Description>
      <![CDATA[
        ]]>
    </Description>
  </GoiParameter>

  <GoiParameter>
        <PropertyName>PARPROFILENUMBER</PropertyName>
        <Address>-1</Address>
        <!-- only in App, will be in controller in the future, we will implement <implemented>/<depricated> tags then -->
        <SubsetOfAddress>false</SubsetOfAddress>
        <Scale>1</Scale>
        <MemoryType>Flash</MemoryType>
        <UserFrom>0</UserFrom>
        <UserTo>32767</UserTo>
        <DigitalFrom>0</DigitalFrom>
        <DigitalTo>32767</DigitalTo>
        <BitRangeStart>0</BitRangeStart>
        <BitRangeEnd>15</BitRangeEnd>
        <MinimumParameterValue>0</MinimumParameterValue>
        <MaximumParameterValue>32767</MaximumParameterValue>
        <ImplementedFirmwareVersion>0.0</ImplementedFirmwareVersion>
        <!--<parameterValue>0</parameterValue>-->
        <enumListValue>
            <!--<string>CLUB_CAR_400A_4kW_Navitas</string>-->
            <string>0</string>
            <!--<string>CLUB_CAR_600A_5kW_Navitas</string>-->
            <string>1</string>
            <!--<string>EZGO_RXV_440A</string>-->
            <string>2</string>
            <!--<string>EZGO_RXV_600A</string>-->
            <string>3</string>
            <!--<string>TAC2_440A_EZGO_L6_72V_5kW_Navitas</string>-->
            <string>4</string>
            <!--<string>TAC2_600A_EZGO_L6_72V_5kW_Navitas</string>-->
            <string>5</string>
            <!--<string>EZGO_RXV2016_440A</string>-->
            <string>6</string>
            <!--<string>EZGO_RXV2016_600A</string>-->
            <string>7</string>
            <!--<string>YDR2_440A</string>-->
            <string>8</string>
            <!--<string>YDR2_600A</string>-->
            <string>9</string>
            <!--<string>EZGO_TXT_440A_4kW_Navitas</string>-->
            <string>10</string>
            <!--<string>EZGO_TXT_600A_5kW_Navitas</string>-->
            <string>11</string>
            <!--<string>INTIMIDATOR_RXV_23_GOI_ACIM_CAN_FOC</string>-->
            <string>12</string>
            <!--<string>BEAST_5kW_CONVERSION</string>-->
            <string>15</string>
            <!--<string>SME_CUB_CADET_600A_8kW</string>-->
            <string>16</string>
            <!--<string>CLUB_CAR_600A_4KW_Navitas</string>-->
            <string>17</string>
            <!--<string>Navitas_DYNO_MODE</string>-->
            <string>18</string>
            <!--<string>Yamaha_G29_600A_5kW_Navitas</string>-->
            <string>19</string>
            <!--<string>Yamaha_G29_440A_4kW_Navitas</string>-->
            <string>20</string>
            <!--<string>Yamaha_G29_600A_4kW_Navitas</string>-->
            <string>21</string>
            <!--<string>DoubleTake_600A_5kW_Navitas</string>-->
            <string>23</string>
            <!--<string>Marshell_600A_4kW_Navitas</string>-->
            <string>24</string>
            <!--<string>Marshell_600A_5kW_Navitas</string>-->
            <string>25</string>
            <!--<string>Marshell_14PassengerBus_600A_7_5kW_Navitas</string>-->
            <string>26</string>
            <!--<string>Columbia_600A_Gen4_8kW</string>-->
            <string>27</string>
            <!--<string>LandMaster_600A_5kw_Navitas</string>-->
            <string>29</string>
            <!--<string>CAN_Networked_4WD_Speed_Matched_Leader_600A_5kW_Navitas</string>-->
            <string>31</string>
            <!--<string>CAN_Networked_4WD_Speed_Matched_Follower_600A_5kW_Navitas</string>-->
            <string>33</string>
            <!--<string>ICON_440A</string>-->
            <string>34</string>
            <!--<string>ICON_600A</string>-->
            <string>35</string>
            <!--<string>AW2044</string>-->
            <string>36</string>
            <!--<string>AW6042</string>-->
            <string>38</string>
            <!--<string>AW9021</string>-->
            <string>40</string>
            <!--<string>AWNEEDANUMBER_7_5kW</string>-->
            <string>42</string>
            <!--<string>TZ2044</string>-->
            <string>44</string>
            <!--<string>TZNEEDANUMBER_7_5kW</string>-->
            <string>46</string>
            <!--<string>Navitas_Chassis_600A_5kW</string>-->
            <string>47</string>
            <!--<string>Columbia_600A_Gen4_5kW</string>-->
            <string>49</string>
            <!--<string>Columbia_440A_Gen4_3kW</string>-->
            <string>50</string>
            <!--<string>Columbia_600A_Gen4_3kW</string>-->
            <string>51</string>
            <!--<string>CVG_Utilitruck_Payloader_440A_8kW</string>-->
            <string>52</string>
            <!--<string>CVG_Utilitruck_Payloader_600A_8kW</string>-->
            <string>53</string>
            <!--<string>CVG_Journeyman_440A_5kW</string>-->
            <string>54</string>
            <!--<string>CVG_Journeyman_600A_5kW</string>-->
            <string>55</string>
            <!--<string>CVG_Emerge_440A_5kW</string>-->
            <string>56</string>
            <!--<string>CVG_Emerge_600A_5kW</string>-->
            <string>57</string>
            <!--<string>Royal_EV_440A/string>-->
            <string>58</string>
            <!--<string>Royal_EV_600A</string>-->
            <string>59</string>
            <!--<string>BINTELLI_440A/string>-->
            <string>60</string>
            <!--<string>BINTELLI_600A</string>-->
            <string>61</string>
            <!--<string>Royal_EV_440A_72V</string>-->
            <string>62</string>
            <!--<string>Royal_EV_600A_72V</string>-->
            <string>63</string>
            <!--<string>ROYAL_EV_600A_72V_6_2kW</string>-->
            <string>65</string>
            <!--<string>ROYAL_EV_600A_72V_6_2kW_NEWAXLE</string>-->
            <string>67</string>
            <!--<string>LandMaster_600A_5kw_Navitas</string>-->
            <string>69</string>
            <!--<string>NAVITAS_OE_440A</string>-->
            <string>70</string>
            <!--<string>NAVITAS_OE_600A</string>-->
            <string>71</string>            
            <!--<string>KODIAK_440A</string>-->
            <string>72</string>
            <!--<string>KODIAK_600A</string>-->
            <string>73</string>
            <!--<string>VIKING_600A_4kW</string>-->
            <string>74</string>
            <!--<string>VIKING_600A_5kW</string>-->
            <string>75</string>
			<!--<string>CSHELL_600A_4kW</string>-->
			<string>76</string>
			<!--<string>CSHELL_600A_5kW</string>-->
			<string>77</string>
			<!--<string>ALWAYZ_600A_4kW</string>-->
			<string>78</string>
			<!--<string>ALWAYZ_600A_5kW</string>-->
			<string>79</string>
			<!--<string>RXV_ELITE_LITHIUM_440A</string>-->
			<string>80</string>
			<!--<string>RXV_ELITE_LITHIUM_600A</string>-->
			<string>81</string>
			<!--<string>CLUB_CAR_400A_4kW_APS_Dual_Throttle</string>-->
			<string>82</string>
			<!--<string>CLUB_CAR_600A_5kW_APS_Dual_Throttle</string>-->
			<string>83</string>
			<!--<string>CLUB_CAR_400A_4kW_APS_FootSw_Throttle</string>-->
			<string>84</string>
			<!--<string>CLUB_CAR_600A_5kW_APS_FootSw_Throttle</string>-->
			<string>85</string>
			<!--<string>Not Implemented</string>-->
            <string>32767</string>
        </enumListValue>
        <enumListName>
            <string>CLUB_CAR_400A_4kW_Navitas</string>
            <!--v8.500-->
            <string>CLUB_CAR_600A_5kW_Navitas</string>
            <!--v8.501-->
            <string>EZGO_RXV_440A</string>
            <!--v8.502-->
            <string>EZGO_RXV_600A</string>
            <!--v8.503-->
            <string>TAC2_440A_EZGO_L6_72V_5kW_Navitas</string>
            <!--v8.504-->
            <string>TAC2_600A_EZGO_L6_72V_5kW_Navitas</string>
            <!--v8.505-->
            <string>EZGO_RXV2016_440A</string>
            <!--v8.506-->
            <string>EZGO_RXV2016_600A</string>
            <!--v8.507-->
            <string>YDR2_440A</string>
            <!--v8.508-->
            <string>YDR2_600A</string>
            <!--v8.509-->
            <string>EZGO_TXT_440A_4kW_Navitas</string>
            <!--v8.510-->
            <string>EZGO_TXT_600A_5kW_Navitas</string>
            <!--v8.511-->
            <string>INTIMIDATOR_RXV_23_GOI_ACIM_CAN_FOC</string>
            <!--v6.712-->
            <string>BEAST_5kW_CONVERSION</string>
            <!--v8.515-->
            <string>SME_CUB_CADET_600A_8kW</string>
            <!--v6.716-->
            <string>CLUB_CAR_600A_4KW_Navitas</string>
            <!--v8.517-->
            <string>Navitas_DYNO_MODE</string>
            <!--v6.718-->
            <string>Yamaha_G29_600A_5kW_Navitas</string>
            <!--v8.519-->
            <string>Yamaha_G29_440A_4kW_Navitas</string>
            <!--v8.520-->
            <string>Yamaha_G29_600A_4kW_Navitas</string>
            <!--v8.521-->
            <string>DoubleTake_600A_5kW_Navitas</string>
            <!--v6.723-->
            <string>Marshell_600A_4kW_Navitas</string>
            <!--v6.724-->
            <string>Marshell_600A_5kW_Navitas</string>
            <!--v6.725-->
            <string>Marshell_14PassengerBus_600A_7_5kW_Navitas</string>
            <!--v6.726-->
            <string>Colubia_600A_Gen4_8kW</string>
            <!--v8.527-->
            <string>LandMaster_600A_5kw_Navitas</string>
            <!--v8.329-->
            <string>CAN_Networked_4WD_Speed_Matched_Leader_600A_5kW_Navitas</string>
            <!--v8.531-->
            <string>CAN_Networked_4WD_Speed_Matched_Follower_600A_5kW_Navitas</string>
            <!--v8.533-->
            <string>ICON_440A</string>
            <!--v8.534-->
            <string>ICON_600A</string>
            <!--v8.535-->
            <string>AW2044</string>
            <!--v6.736-->
            <string>AW6042</string>
            <!--v6.738-->
            <string>AW9021</string>
            <!--v6.740-->
            <string>AWNEEDANUMBER_7_5kW</string>
            <!--v6.742-->
            <string>TZ2044</string>
            <!--v6.744-->
            <string>TZNEEDANUMBER_7_5kW</string>
            <!--v6.746-->
            <string>Navitas_Chassis_600A_5kW</string>
            <!--v8.547-->
            <string>Columbia_600A_Gen4_5kW</string>
            <!--v8.549-->
            <string>Columbia_440A_Gen4_3kW</string>
            <!--v8.550-->
            <string>Columbia_600A_Gen4_3kW</string>
            <!--v8.551-->
            <string>CVG_Utilitruck_Payloader_440A_8kW</string>
            <!--v8.552-->
            <string>CVG_Utilitruck_Payloader_600A_8kW</string>
            <!--v6.353-->
            <string>CVG_Journeyman_440A_5kW</string>
            <!--v8.554-->
            <string>CVG_Journeyman_600A_5kW</string>
            <!--v6.355-->
            <string>CVG_Emerge_440A_5kW</string>
            <!--v8.556-->
            <string>CVG_Emerge_600A_5kW</string>
            <!--v6.357-->
            <string>Royal_EV_440A</string>
            <!--v8.558-->
            <string>Royal_EV_600A</string>
            <!--v6.759-->
            <string>BINTELLI_440A</string>
            <!--v8.560-->
            <string>BINTELLI_600A</string>
            <!--v8.561-->
            <string>Royal_EV_440A_72V</string>
            <!--v8.562-->
            <string>Royal_EV_600A_72V</string>
            <!--v8.563-->
            <string>ROYAL_EV_600A_72V_6_2kW</string>
            <!--v8.565-->
            <string>ROYAL_EV_600A_72V_6_2kW_NEWAXLE</string>
            <!--v8.567-->
            <string>LandMaster_600A_5kw_Navitas</string>
            <!--v8.329-->
            <string>NAVITAS_OE_440A</string>
            <!--v8.570-->
            <string>NAVITAS_OE_600A</string>
            <!--v8.571-->
            <string>KODIAK_440A</string>
            <!--v8.572-->
            <string>KODIAK_600A</string>
            <!--v8.573-->
            <string>VIKING_600A_4kW</string>
            <!--v8.574-->
			<string>VIKING_600A_5kW</string>
			<!--v8.575-->
			<string>CSHELL_600A_4kW</string>
			<!--v8.576-->
			<string>CSHELL_600A_5kW</string>
			<!--v8.577-->
			<string>ALWAYZ_600A_4kW</string>
			<!--v8.578-->
			<string>ALWAYZ_600A_5kW</string>
			<!--v8.579-->
			<string>RXV_ELITE_LITHIUM_440A</string>
			<!--v8.580-->
			<string>RXV_ELITE_LITHIUM_600A</string>
			<!--v8.581-->
			<string>CLUB_CAR_400A_4kW_APS_Dual_Throttle</string>
			<!--v11.482-->
			<string>CLUB_CAR_600A_5kW_APS_Dual_Throttle</string>
			<!--v11.483-->
			<string>CLUB_CAR_400A_4kW_APS_FootSw_Throttle</string>
			<!--v11.484-->
			<string>CLUB_CAR_600A_5kW_APS_FootSw_Throttle</string>
			<!--v11.485-->
			<string>Not Implemented</string>
        </enumListName>
        <Name>Profile Number</Name>
        <Description>
            <![CDATA[
        ]]>
        </Description>
    </GoiParameter>
</ArrayOfGoiParameter>
<!--
        Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A)
        <b>Description:</b>(Images:TAC2_EZGO_RXV23.png,TAC2_EZGO_RXV35.png) The run/tow switch when switched to tow, connects the battery voltage to this input. When switched to tow the controller will disengage the parking brake when the key is turned from off to on.
        Binding:PARPROFILENUMBER.enumListName(EZGO_TXT_440A_4kW_Navitas,EZGO_TXT_600A_5kW_Navitas,CLUB_CAR_400A_4kW_Navitas,CLUB_CAR_600A_5kW_Navitas)
        <b>Description:</b>(Images:TAC2_ClubCar_Precedent_EZGO_TXT.png,TAC2_EZGO_1268_Resistive_ITS.png,TAC2_TSX.png) On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
        Binding:PARPROFILENUMBER.enumListName(Yamaha_G29_440A_4kW_Navitas,Yamaha_G29_600A_4kW_Navitas)
        <b>Description:</b>(Images:TAC2_Yamaha_G29.png) On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
        Binding:PARPROFILENUMBER.enumListName(YDR2_440A,YDR2_600A)
        <b>Description:</b>(Images:TAC2_Yamaha_YDRE2.png) On this vehicle the run/tow switch is the low current power supply for this controller. When switched to RUN the controller is powered. When switched to tow the controller has no power (free to be towed).
        -->
`