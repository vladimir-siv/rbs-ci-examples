#include <iostream>
#include <cstdlib>
#include <string>
using namespace std;

int main(int argc, char* argv[])
{
	if (argc != 2)
	{
		cerr << "No arguments or too many specified. Usage: 'ld <path>'." << endl;
		return 1;
	}

	string cmd("dir ");
	cmd += argv[1];
	system(cmd.c_str());

	return 0;
}
