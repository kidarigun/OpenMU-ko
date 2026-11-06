# OpenMU 한국어판 (OpenMU-ko)

[MUnique/OpenMU](https://github.com/MUnique/OpenMU)를 한국어로 옮긴 포크입니다.
OpenMU는 MMORPG "MU Online"의 서버를 처음부터 새로 작성한 오픈 소스
프로젝트이며, 이 저장소는 그 위에 다음을 더합니다.

- **관리자 패널 한국어:** 대시보드, 게임 구성, 계정, 공성전, Discord 연동 등 관리자
  패널 전체 문구. 언어 선택에서 **한국어**를 고르면 됩니다.
- **게임 데이터 한국어:** 아이템, 몬스터, 스킬, 맵, 퀘스트, 이벤트 이름과 설명.
- **플레이어 메시지 한국어:** 서버가 플레이어에게 보내는 메시지. 게임 안에서
  `/language ko`를 입력하면 한국어로 바뀝니다.
- **클라이언트 런처 한국어:** `launcher.config`에 `<UiCulture>ko</UiCulture>`를
  넣으면 Windows 표시 언어와 관계없이 한국어로 표시됩니다.

용어는 [한국어 용어집](docs/korean-localization/terminology-ko.md)을 따르며,
`tests/MUnique.OpenMU.Tests/Localization`의 테스트가 번역의 키·자리표시자·명령어
보존을 검사합니다.

### 게임 클라이언트

이 저장소에는 게임 클라이언트가 포함되어 있지 않습니다. OpenMU가 권장하는 오픈 소스
클라이언트 [sven-n/MuMain](https://github.com/sven-n/MuMain)은 원작 클라이언트
소스에서 파생되었고 별도 라이선스가 없으며 원작 게임 리소스를 포함하므로, 이
프로젝트에서 재배포하지 않습니다. 클라이언트 한국어화는 MuMain 쪽에 기여하는
방식으로 진행합니다.

### 원본과의 관계와 라이선스

원본 OpenMU의 전체 Git 히스토리를 그대로 유지하고 있으며, 원본 프로젝트의 MIT
라이선스([LICENSE](LICENSE), Copyright 2017 MUnique)를 따릅니다. 원본의 새 변경은
`upstream` 원격(`https://github.com/MUnique/OpenMU.git`)에서 가져와 병합합니다.
서버 실행 방법과 그 밖의 문서는 아래 원본 README와 [docs-website](docs-website)를
참고하세요.

---

# OpenMU Project

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/d0f57e29e7524dadb677561389256d8b)](https://app.codacy.com/gh/MUnique/OpenMU/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MUnique/OpenMU)
[![Discord chat](https://img.shields.io/discord/669595902750490698?logo=discord)](https://discord.gg/2u5Agkd)

| Platform       |Build Status          |
|----------------|----------------------|
| Linux          | [![Build Status](https://github.com/MUnique/OpenMU/actions/workflows/dotnetcore.yml/badge.svg?branch=master)](https://github.com/MUnique/OpenMU/actions/workflows/dotnetcore.yml) |
| Linux (Docker) | [![Docker Build Status](https://github.com/MUnique/OpenMU/actions/workflows/docker.yml/badge.svg?branch=master)](https://hub.docker.com/r/munique/openmu)  |

| NuGet Packages |   |
|----------------|---|
| MUnique.OpenMU.Network | [![NuGet Badge](https://img.shields.io/nuget/v/MUnique.OpenMU.Network)](https://www.nuget.org/packages/MUnique.OpenMU.Network/) |
| MUnique.OpenMU.Network.Packets | [![NuGet Badge](https://img.shields.io/nuget/v/MUnique.OpenMU.Network.Packets)](https://www.nuget.org/packages/MUnique.OpenMU.Network.Packets/) |

This project aims to create an easy to use, extendable and customizable server
for a MMORPG called "MU Online".
The server supports multiple versions of the game, but the main focus is
version of Season 6 Episode 3 using the ENG (english) protocol. Additionally,
the long-term focus is on the [open source client](https://github.com/sven-n/MuMain)
which supports a slightly extended network protocol.
However, parts of the software can also be suitable for the development of
other games, even for other kind of games.

The code is a complete rewrite from scratch - it's not based on pre-existing
projects, and it's also explicitly not based on decompiled server sources or
their countless derivates.

There also exists a [blog](https://munique.net) which may contain some valuable
information about this development.

## Current project state

This project is currently under development without any release.
You can try the current state by using the available docker image, see
[Run with Docker](docs-website/docs/getting-started/docker.md).

## Documentation

The documentation lives in the [docs-website](docs-website) folder and is built
as a website with [Docusaurus](https://docusaurus.io/). The links below point to
its sources, so they work on GitHub as well.

| | |
|---|---|
| [Getting started](docs-website/docs/getting-started/requirements.md) | Requirements, ports, running with Docker or from source, connecting a game client, test accounts |
| [Deployment](docs-website/docs/deployment/overview.md) | The deployment variants, HTTPS, start parameters and environment variables |
| [Admin panel](docs-website/docs/admin-panel/overview.md) | One page per screen of the server's user interface, plus task-oriented how-tos |
| [Server features](docs-website/docs/server-features/bots.md) | Features with their own configuration, e.g. the server-side AI bots |
| [Development](docs-website/docs/development/architecture.md) | Architecture, solution structure, the plugin system, contributing |
| [Reference](docs-website/docs/reference/ports.md) | Ports and the generated packet documentation |

The code-bound technical documentation stays next to the code: see [docs](docs)
for the packet descriptions and the game mechanics, and the `Readme.md` of the
individual projects under [src](src).

To run the website locally:

```bash
cd docs-website
npm install
npm start
```

## Licensing

This project is released under the MIT license (see LICENSE file).

## Contributions

Contributions are welcome — from developers and non-developers alike. Please
read [CONTRIBUTING.md](CONTRIBUTING.md) before you start, and don't hesitate to
ask in our [discord channel](https://discord.gg/2u5Agkd) or by submitting an
issue.
