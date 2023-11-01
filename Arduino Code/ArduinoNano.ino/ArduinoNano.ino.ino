////////////////////////////////////////
// Libraries
////////////////////////////////////////

#include <LiquidCrystal_I2C.h> // Library for LCD

#include <Keypad.h> //library for keyboard

////////////////////////////////////////
// LED traffic light items
////////////////////////////////////////

#define PIN_RED 4     // The Arduino pin connected to R pin of traffic light module
#define PIN_YELLOW 3  // The Arduino pin connected to Y pin of traffic light module
#define PIN_GREEN 2   // The Arduino pin connected to G pin of traffic light module

#define RED_TIME 2000     // RED time in millisecond
#define YELLOW_TIME 1000  // YELLOW time in millisecond
#define GREEN_TIME 2000   // GREEN time in millisecond

#define RED  0    // Index in array
#define YELLOW 1  // Index in array
#define GREEN  2  // Index in array

const int pins[] = { PIN_RED, PIN_YELLOW, PIN_GREEN };
const int times[] = { RED_TIME, YELLOW_TIME, GREEN_TIME };
const String colours[] = {"RED", "YELLOW", "GREEN"};

unsigned long last_time = 0;

int light = RED;  // start with RED light

//////////////////////////////////////
// LCD items
//////////////////////////////////////
LiquidCrystal_I2C lcd(0x27, 20, 4); // create the LCDobject: I2C address 0x27, 20 column and 4 rows

/////////////////////////////////////
// Keyboard items
/////////////////////////////////////

const byte ROWS = 4;                  //four rows
const byte COLS = 4;                  //four columns

                                      //Define the keys to show
char keys[ROWS][COLS] = {
  {'1','2','3','A'},
  {'4','5','6','B'},
  {'7','8','9','C'},
  {'*','0','#','D'}
};

byte rowPins[ROWS] = {12, 11, 10, 9};    //connect keypad 1 to D5, Keypad 2 to D6 and so on
byte colPins[COLS] = {8, 7, 6, 5}; //connect keypad 5 to D9, Keypad 6 to D10 and so on

Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, ROWS, COLS ); //Create the Keypad object




void setup() {
  
  lcd.init(); //initialize the lcd
  lcd.backlight(); //open the backlight 

  pinMode(PIN_RED, OUTPUT);
  pinMode(PIN_YELLOW, OUTPUT);
  pinMode(PIN_GREEN, OUTPUT);
  
  traffic_light_on(light); //Initialise with some data
  last_time = millis();
  
  // initialize serial communication at 9600 baud (9600 symbols a second)
  Serial.begin(9600);
  
  

  lcd.setCursor(0, 0);            // move cursor the first row
  lcd.print("LCD 20x4");          // print message at the first row
  lcd.setCursor(0, 1);            // move cursor to the second row
  lcd.print("I2C Address: 0x27"); // print message at the second row
  lcd.setCursor(0, 2);            // move cursor to the third row
  lcd.print("Light on");          // print message at the third row
  

}

// the loop function runs over and over again forever
void loop() {
  if ((millis() - last_time) > times[light]) {
    light++;
    if (light >= 3)
      light = RED;  // new circle

    traffic_light_on(light);
    last_time = millis();
  }

  char key = keypad.getKey();
  
  if (key){
    clearLCDLine(1);                // clear LCD
    writeToLCD((String)key, 0, 1);
  }
}

void traffic_light_on(int light) {

  for (int i = RED; i <= GREEN; i++) 
  {
    if (i == light)
    {
      digitalWrite(pins[i], HIGH);    // turn on
      clearLCDLine(3);                // clear LCD
      writeToLCD(colours[light], 0, 3);
    }
    else
    {
      digitalWrite(pins[i], LOW);  // turn off
    }
      
  }
}

//Write to LCD
void writeToLCD(String MSG, int pos, int line)
{
  lcd.setCursor(pos, line);
  lcd.print(MSG);
  Serial.println(MSG);
}

//clear only lines to be rewritten
void clearLCDLine(int line)
{
  lcd.setCursor(0, line);
  for (int n = 0; n < 20; n++) // 20 indicates 0-20 symbols in line.
  {
    lcd.print(" ");
  }
}

/*
  SerialEvent occurs whenever new data comes in the hardware serial RX. This
  routine is run between each time loop() runs, so using delay inside loop can
  delay response. Multiple bytes of data may be available. We only send data
  once.
*/
void serialEvent() 
{  
  char inChar;
  String DataString = ""; // a String to hold incoming data
  DataString.reserve(21); // reserve 21 bytes for the inputString:

  delay(100); //Add delay not to miss first byte
    
  while (Serial.available()) 
  {
    // get the new byte:
    inChar = (char)Serial.read();
    // add it to the DataString:
    DataString += inChar;    
  }
  clearLCDLine(2);                // clear LCD
  writeToLCD(DataString, 0, 2);   // write data
  DataString = "";                // Empty string
}

