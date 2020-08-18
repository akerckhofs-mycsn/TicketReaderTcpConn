# Setup

## WSL

Follow the [installation guide by Microsoft](https://docs.microsoft.com/en-us/windows/wsl/install-win10). Do __not__ update to WSL2 as this breaks USB device passthrough.

Install the Alpine distro either through the [Microsoft store](https://www.microsoft.com/fr-be/p/alpine-wsl/9p804crf0395?rtc=1&activetab=pivot:overviewtab) or [the github repo](https://github.com/yuk7/AlpineWSL).

### Alpine setup

Install the necessary packages.
```bash
apk add ser2net nano
```

Run ser2net with a specific config line. It is important to use **-C** as CLI param instead of **-c**.

/dev/**ttyS4** is the equivalent of the **COM4** port, so this needs to be verified against the device manager on the Windows host.
```bash
ser2net -C <port>:raw:0:/dev/<ttyS4>:9600
```

### Test

```bash
apk add socat
socat socat -d -d - TCP4:127.0.0.1:port
```

