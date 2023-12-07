#include "FootballFrontierInternational.h"
FootballFrontierInternational::FootballFrontierInternational() {
}
FootballFrontierInternational::~FootballFrontierInternational() {

}

void FootballFrontierInternational::MakeTeam(std::string _Name) {
	Team* _team = new Team();
	_team->SetName(_Name);
	teams.push_back(_team);
	TeamIndexCounter();
}

void FootballFrontierInternational::DeleteTeam(Team* team) {
	bool _choose = false;
	if (team->PlayerAmount() >= 1) {
		Team* overig = GetTeam(_choose);
		overig->EvacuatePlayer(team);
	}
	teams.erase(remove(teams.begin(), teams.end(), team));
	std::string removing = team->GetName() + ".txt";
	delete(team);


	TeamIndexCounter();


	if (remove(removing.c_str()) != 0)
	{
		perror("Error deleting file");
	}
	else
	{
		puts("File successfully deleted");
	}
}

/**
 * Prints a message
 * @param i -1 is choosing -2 is viewing
 */

Team* FootballFrontierInternational::GetTeam(int i){
	if (i > teams.size() || i < 0) {
		if (i == -1)
		{
			i = 0;
			std::cout << "the teams:\n";
			for (Team* team : teams)
			{
				std::cout << team->GetName() << " " << team->GetIndex() << "\n";
				i += 1;
			}
			std::cout << "pick a team\n";
			std::cin >> i;
		}
		else {
			for (Team* team : teams)
			{
				std::cout << "the teams:\n";
				for (Team* team : teams)
				{
					std::cout << team->GetName() << " " << team->GetIndex() << "\n";
					i += 1;
				}
			}
			system("CLS");
			return teams[0];
		}
	}
	system("CLS");
	return teams[i];
}

void Handler::WrongInput(bool inputCheck) {
	system("CLS");
	if (std::cin.fail())
	{
		run = true;
		std::cout << "wrong input, type an int to go on.\n";
		int ind;
		while (run) {
			run = false;
			std::cin >> ind;
			if (std::cin.fail()) {
				std::cin.clear();
				std::cin.ignore();
				run = true;
			}
		}
		run = true;
		return;
	}
	if (inputCheck == false) {
		std::cout << "wrong input try again.\n";
		system("pause");
	}
}

//Team* FootballFrontierInternational::GetTeam(int retrievingIndex,bool _check) {
//	system("CLS");
//	std::cout << "the teams\n";
//	int i = 0;
//	std::vector <int> index;
//	for (i; i < teams.size(); i++) {
//		if (teams.at(i)->PlayerAmount() >= 1 || _check == true) {
//			std::cout << teams.at(i)->GetName() << "(" << teams.at(i)->GetIndex() << ")\n";
//			index.push_back(i);
//		}
//	}
//	std::cout << "pick a team\n";
//	int input;
//	std::cin >> input;
//#pragma region CheckInput
//	bool checkInput = false;
//	if (std::cin.fail()) {
//		checkInput = true;
//		std::cout << "wrong input, type an int to go on.\n";
//		int ind;
//		while (checkInput) {
//			checkInput = false;
//			std::cin >> ind;
//			if (std::cin.fail()) {
//				std::cin.clear();
//				std::cin.ignore();
//				checkInput = true;
//			}
//		}
//		return GetTeam(retrievingIndex, _check);
//	}
//	for (int i : index) {
//		if (i == input) {
//			checkInput = true;
//		}
//	}
//	if (checkInput == false) {
//		system("CLS");
//		std::cout << "wrong input\n";
//		system("pause");
//		return GetTeam(retrievingIndex, _check);
//	}
//#pragma endregion
//	system("CLS");
//	return teams[input];
//}



void FootballFrontierInternational::MakePlayer(std::string _Name, int _Age, std::string _Position ,  int choose) {
		Player* make = new Player();
		make->SetName(_Name);
		make->SetAge(_Age);
		make->SetPosition(_Position);
		Team* team = GetTeam(choose);
		team->AddPlayer(make);
}

void FootballFrontierInternational::TeamIndexCounter() {
int ind = 0;
for (Team* team : teams) 
	{
		team->SetIndex(ind);
		ind += 1;
	}
}

void FootballFrontierInternational::Save() {
	std::ofstream file1;
	file1.open("TEAMS.txt");
	for (Team* team : teams) {
		file1 << team->GetName() << "\n";
		team->Save();
	}
	file1.close();
}
