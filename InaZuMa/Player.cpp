#include "Player.h"
Player::Player() {

}

Player::~Player() {

}

int Player::GetAge() {
	return age;
}

void Player::SetAge(int _Age) {
	if (_Age > 40 || _Age < 9) {
		system("CLS");
		std::cout << "fix je leeftijd\n";
		std::cin >> _Age;
		if (std::cin.fail()) {
		}
		SetAge(_Age);
		return;
	}
	age = _Age;
	system("CLS");
}

void Player::SetName(std::string _Name) {
	name = _Name;
	system("CLS");
}

std::string Player::GetName() {
	return name;
}

void Player::SetPosition(std:: string _Position) {
	if (_Position != "GK" && _Position != "DF" && _Position != "MF" && _Position != "AT") {
		system("CLS");
		std::cout << "fix je positie\n";
		std::cin >> _Position;
		SetPosition(_Position);
		return;
	}
	else
	{
	system("CLS"); 
	position = _Position;			
	}
}

std::string Player::GetPosition() {
	return position;
}

void Player:: GetStats() {
	std::cout << "NAME:" << GetName() << "\n";
	std::cout << "AGE:" << GetAge() << "\n";
	std::cout << "POSITION:"<< GetPosition() << "\n";
}