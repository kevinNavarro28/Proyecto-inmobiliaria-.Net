-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-11-2024 a las 18:41:54
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliarianavarro`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id` int(11) NOT NULL,
  `Fecha_Inicio` date NOT NULL,
  `Fecha_Fin` date NOT NULL,
  `Monto` double NOT NULL,
  `InmuebleId` int(11) NOT NULL,
  `InquilinoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id`, `Fecha_Inicio`, `Fecha_Fin`, `Monto`, `InmuebleId`, `InquilinoId`) VALUES
(46, '2024-11-26', '2025-04-26', 125000, 19, 13),
(47, '2024-10-26', '2025-02-26', 150000, 18, 16);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id` int(11) NOT NULL,
  `Tipo` varchar(200) NOT NULL,
  `Direccion` varchar(200) NOT NULL,
  `Uso` varchar(200) NOT NULL,
  `Precio` double NOT NULL,
  `Cantidad_Ambientes` int(11) NOT NULL,
  `Superficie` int(11) NOT NULL,
  `Latitud` double NOT NULL,
  `Longitud` double NOT NULL,
  `Estado` varchar(200) NOT NULL,
  `PropietarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id`, `Tipo`, `Direccion`, `Uso`, `Precio`, `Cantidad_Ambientes`, `Superficie`, `Latitud`, `Longitud`, `Estado`, `PropietarioId`) VALUES
(13, 'Casa', 'San Martin 2340', 'Vivienda', 1000000, 4, 75000, 64890, 87000, 'Disponible', 15),
(14, 'Edificio', 'San Luis 890', 'Comercial', 45000000, 4, 3213, 31231, 312312, 'No Disponible', 16),
(16, 'Departamento', '25 de Mayo 2525', 'Vivienda', 3700000, 5, 213, 213, 41, 'No Disponible', 12),
(17, 'Casa', '25 de mayo 3289', 'Comercial', 2500000, 2, 3000, 2000, 2000, 'Disponible', 11),
(18, 'Departamento', 'barrio 118', 'Residencial', 2000000, 2, 344, 433, 100, 'Disponible', 12),
(19, 'Local', 'Junin 2090', 'Residencial', 25000000, 2, 300, 300, 100, 'Disponible', 16),
(20, 'Depósito', '25 de Agosto ', 'Comercial', 9000000, 5, 2000, 2000, 100, 'Disponible', 16);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(255) NOT NULL,
  `Apellido` varchar(255) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Nacimiento` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Direccion`, `Nacimiento`) VALUES
(12, 'Juan', 'Romero', 21345987, 2665098712, 'Juan@gmail.com', 'Catamarca 245', '2024-11-19'),
(13, 'Andrea', 'Suarez', 17123199, 2664789122, 'AndreaSuarez@hotmail.com', 'Buenos Aires 1209', '1998-02-26'),
(14, 'Fernando', 'Coronel', 21345098, 2664590981, 'Fernando@gmail.com', 'Malvinas 2340', '1992-03-06'),
(16, 'Pablo ', 'Cortez', 28916789, 2664567819, 'Pablo@mail.com', '9 de julio 1278', '1987-05-17');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `Id` int(11) NOT NULL,
  `Fecha_Pago` date NOT NULL,
  `Importe` double NOT NULL,
  `ContratoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`Id`, `Fecha_Pago`, `Importe`, `ContratoId`) VALUES
(53, '2024-11-26', 125000, 46);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(255) NOT NULL,
  `Apellido` varchar(255) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Nacimiento` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Direccion`, `Nacimiento`) VALUES
(11, 'Facundo', 'Fernandez', 21567091, 2665897451, 'FaFe_21@homtail.com', 'barrio cruz del sur manzana 23 casa 32', '2005-06-10'),
(12, 'Esteban', 'Carrizo', 23871982, 2664123456, 'Estecarrizo_12@hotmail.com', 'barrio 23 manzana 1 casa 55', '1997-05-28'),
(15, 'Pablo', 'Gonzales', 24890178, 2665789019, 'Pablo26@hotmail.com', 'Barrio amep 256', '1986-02-02'),
(16, 'Pablo', 'Garcia', 31875230, 2667980178, 'Pablo@hotmail.com', 'departamento 3 piso 2', '2022-06-30');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(200) NOT NULL,
  `Apellido` varchar(200) NOT NULL,
  `Email` varchar(200) NOT NULL,
  `Clave` varchar(200) NOT NULL,
  `Avatarruta` varchar(200) NOT NULL,
  `Rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nombre`, `Apellido`, `Email`, `Clave`, `Avatarruta`, `Rol`) VALUES
(40, 'Kevin', 'Navarro', 'kevin@mail.com', 'imgnRr/3fTU/PSVR93gFbjFAgw7qPvZUtmzPS7A7OZk=', '/Uploads\\foto_40.jpg', 2),
(44, 'jose', 'Cortez', 'jose@mail.com', 'imgnRr/3fTU/PSVR93gFbjFAgw7qPvZUtmzPS7A7OZk=', '/Uploads\\foto_44.jpg', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idInmueble` (`InmuebleId`),
  ADD KEY `idInquilino` (`InquilinoId`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idPropietarios` (`PropietarioId`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idContrato` (`ContratoId`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=54;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`InquilinoId`) REFERENCES `inquilinos` (`Id`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`InmuebleId`) REFERENCES `inmuebles` (`Id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`PropietarioId`) REFERENCES `propietarios` (`Id`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`ContratoId`) REFERENCES `contratos` (`Id`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
