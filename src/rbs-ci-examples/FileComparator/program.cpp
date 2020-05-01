#include <iostream>
#include <cstdlib>
#include <string>
using namespace std;

int main(int argc, char* argv[])
{
	if (argc != 3)
	{
		cerr << "Not enough or too many arguments specified. Usage: 'cmp <file1> <file2>'." << endl;
		return 1;
	}

	string cmd("fc /L /N /T ");
	cmd += '\"';
	cmd += argv[1];
	cmd += "\" \"";
	cmd += argv[2];
	cmd += "\"";
	system(cmd.c_str());

	return 0;
}
