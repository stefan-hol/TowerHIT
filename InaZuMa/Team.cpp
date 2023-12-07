#include "Team.h"
Team:: Team() {

}
Team:: ~Team(){

}
void Team::EvacuatePlayer(Team* team) {
	for (Player* play : team->TeamLeden)
	{
		AddPlayer(play);
	}
}

void Team::AddPlayer(Player* _player) {
	TeamLeden.push_back(_player);
}

void Team::DeletePlayer(Player* _player, bool death) {
	TeamLeden.erase(remove(TeamLeden.begin(),TeamLeden.end(), _player));
	if (death == true) {
		delete(_player);
	}
}

std::string Team::GetName(){
	return name;
}

Player* Team::GetTeamMember(bool stats) {
	int i = 0;
	for (i; i < TeamLeden.size(); i++) {
		std::cout << "(" << i << ")" << TeamLeden.at(i)->GetName() << "\n";
	}
	int player = 0;
	if (stats == true) {
		std::cout << "choose wich Player to select\n";
		std::cin >> player;
		if (std::cin.fail() && player > TeamLeden.size())
		{
			system("CLS");
			if (player > TeamLeden.size()) {
				std::cout << "wrong input.\n";
				system("pause");
				return GetTeamMember(stats);;
			}
			bool run = true;
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
			GetTeamMember(stats);
		}
	}
	return TeamLeden.at(player);
}

void Team::SetName(std::string _name) {
	name = _name;
}

int Team:: PlayerAmount() {
	return TeamLeden.size();
}

void Team::SetIndex(int ind) {
	index = ind;
}

int Team::GetIndex() {
	return index;  
}

void Team::Save() {
	std::ofstream file1;
	file1.open(GetName() + ".txt");
	for (Player* player : TeamLeden) {
		file1 << player->GetName() << " " << player->GetAge() << " " << player->GetPosition() << "\n";
	}
	file1.close();
}