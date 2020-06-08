export * from './calculations.service';
import { CalculationsService } from './calculations.service';
export * from './oidcConfiguration.service';
import { OidcConfigurationService } from './oidcConfiguration.service';
export * from './words.service';
import { WordsService } from './words.service';
export const APIS = [CalculationsService, OidcConfigurationService, WordsService];
