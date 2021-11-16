VERSION = "0.0.0"

NONRUNNABLE_PROJECTS := "\
	ContactCoffee.Data\
	"

RUNNABLE_PROJECTS := \
	ContactCoffee.Processor\
	ContactCoffee.Web\
	
EMULATOR_IMAGE_NAME := "test-linux-emulator"

.PHONY: all clean restore build run

all: clean restore build

clean: $(RUNNABLE_PROJECTS:%=clean-%)

clean-%:
	dotnet clean $*

restore: $(RUNNABLE_PROJECTS:%=restore-%)

restore-%:
	dotnet restore $*

build: $(RUNNABLE_PROJECTS:%=build-%)

build-%:
	dotnet build $*

run: start-emulator $(RUNNABLE_PROJECTS:%=run-%)

run-%:
	dotnet run -p $* & \
		echo $$! > $*.pid

stop: $(RUNNABLE_PROJECTS:%=stop-%)

stop-%: | $(*:%=%.pid) stop-emulator
	kill $$(cat $*.pid) && \
		rm $*.pid

start-emulator:
	docker start $(EMULATOR_IMAGE_NAME)
	sleep 15

stop-emulator:
	docker stop $(EMULATOR_IMAGE_NAME)
