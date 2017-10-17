#include<iostream>
#include<string>
#include<conio.h>
#include<math.h>

struct STUDENT
{
	char sId[10];
	char sName[30];
	char sDoB[12];
	char sEmail[30];
	char sPhone[15];
	char sAddress[30];
	char sBatch[5];
};

struct STUDENT students[100];
STUDENT student;
int numOfStudent = 0;

void add()
{
	fflush(stdin);
	printf("\nInput student ID: ");
	scanf_s("%s", &students[numOfStudent].sId, sizeof(students[numOfStudent].sId));

	fflush(stdin);
	printf("\nInput student Name: ");
	gets_s(students[numOfStudent].sName);

	fflush(stdin);
	printf("\nInput student DoB: ");
	gets_s(students[numOfStudent].sDoB);

	fflush(stdin);
	printf("\nInput student Email: ");
	gets_s(students[numOfStudent].sEmail);

	fflush(stdin);
	printf("\nInput student Phone: ");
	gets_s(students[numOfStudent].sPhone);

	fflush(stdin);
	printf("\nInput student Address: ");
	gets_s(students[numOfStudent].sAddress);

	fflush(stdin);
	printf("\nInput student Batch: ");
	gets_s(students[numOfStudent].sBatch);
	numOfStudent++;
	fflush(stdin);
}

void display_all()
{
	for (int i = 0; i < numOfStudent; i++)
	{
		printf("\nStudent informations number of : %d", i + 1);
		printf("\n - ID: %s", students[i].sId);
		printf("\n - Name: %s", students[i].sName);
		printf("\n - DoB: %s", students[i].sDoB);
		printf("\n - Email: %s", students[i].sEmail);
		printf("\n - Phone: %s", students[i].sPhone);
		printf("\n - Address: %s", students[i].sAddress);
		printf("\n - Batch: %s\n", students[i].sBatch);
	}
}

void search_all(){
	fflush(stdin);
	printf("\nSearch Name: ");
	gets_s(student.sName);
	fflush(stdin);
	printf("\nResult of search :\n");
	for (int i = 0; i < numOfStudent; i++)
	{
		if (!strcmp(students[i].sName,student.sName)){
			printf("\nStudent informations number of : %d", i + 1);
			printf("\n - ID: %s", students[i].sId);
			printf("\n - Name: %s", students[i].sName);
			printf("\n - DoB: %s", students[i].sDoB);
			printf("\n - Email: %s", students[i].sEmail);
			printf("\n - Phone: %s", students[i].sPhone);
			printf("\n - Address: %s", students[i].sAddress);
			printf("\n - Batch: %s\n", students[i].sBatch);
		}
	}
	fflush(stdin);
}

void delete_student(){
	fflush(stdin);
	printf("\nStudent Id : ");
	gets_s(student.sId);
	fflush(stdin);
	bool boo = false;
	for (int i = 0; i < numOfStudent; i++)
	{
		if (!strcmp(students[i].sId, student.sId)){
			boo = true;
		}
		if (boo)
		{
			students[i] = students[i + 1];
		}
	}
	if (boo)
	{
		numOfStudent--;
		printf("\nDelete Success. ");
	}
	else
	{
		printf("\nDelete fail. ");
	}
	fflush(stdin);
}


void update_student(){
	fflush(stdin);
	printf("\nInput student ID: ");
	scanf_s("%s", student.sId, sizeof(student.sId));

	fflush(stdin);
	printf("\nInput student Name: ");
	gets_s(student.sName);

	fflush(stdin);
	printf("\nInput student DoB: ");
	gets_s(student.sDoB);

	fflush(stdin);
	printf("\nInput student Email: ");
	gets_s(student.sEmail);

	fflush(stdin);
	printf("\nInput student Phone: ");
	gets_s(student.sPhone);

	fflush(stdin);
	printf("\nInput student Address: ");
	gets_s(student.sAddress);

	fflush(stdin);
	printf("\nInput student Batch: ");
	gets_s(student.sBatch);

	bool boo = false;
	for (int i = 0; i < numOfStudent; i++)
	{
		if (!strcmp(students[i].sId, student.sId)){
			boo = true;
			students[i] = student;
			break;
		}
	}
	if (boo)
	{
		numOfStudent--;
		printf("\nUpdate Success.");
	}
	else
	{
		printf("\nUpdate fail.");
	}
	fflush(stdin);
}
int main()
{
	int option = 0;
	char x;
	do
	{
		fflush(stdin);
		system("cls");
		printf("\n1. Add student\n2. View all students\n3. Search students");
		printf("\n4. Delete student\n5. Update student\n6. Exit\n");
		printf("\nPlease input your option (1-6): ");
		scanf_s("%d", &option);
		fflush(stdin);
		switch (option)
		{
		case 1:
			add();
			break;
		case 2:
			display_all();
			getchar();
			break;
		case 3:
			search_all();
			getchar();
			break;
		case 4:
			delete_student();
			getchar();
			break;
		case 5:
			update_student();
			getchar();
			break;
		case 6:
			fflush(stdin);
			printf("\nDo you want to exit (y/n)?: ");
			scanf_s("%c", &x);
			if (x != 'y')
				option = 0;
			break;
		default:
			fflush(stdin);
			printf("\nYour option is not correct. Please try again\n");
			getchar();
			break;
		}
	} while (option != 6);
}
