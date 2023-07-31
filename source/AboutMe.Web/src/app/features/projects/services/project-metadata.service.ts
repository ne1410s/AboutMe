import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { ProjectMetadata } from '../models/project-metadata.class';

@Injectable({ providedIn: 'root' })
export class ProjectMetadataService {
  private readonly projects: ProjectMetadata[] = [
    new ProjectMetadata(new Date(2018, 8), 'ACME', 'acme', 'Free Domain Validation SSL certificates using ACME v2.'),
    new ProjectMetadata(
      new Date(2022, 8),
      'Comanche',
      'comanche',
      'Swagger for the CLI! Deigns to combine: XML-, attribute-, and reflection-based documentation sources.'
    ),
    new ProjectMetadata(
      new Date(2016, 1),
      'Crypto Stream',
      'crypto-stream',
      'The limitations of CBC (..!) and how CTR can step in.'
    ),
    new ProjectMetadata(
      new Date(2020, 2),
      'Custom Elements',
      'custom-elements',
      'Shadow DOM + CSS3 variables can provide a blend of OOTB functionality and customisability.'
    ),
    new ProjectMetadata(
      new Date(2014, 11),
      'Fluent Errors',
      'fluent-errors',
      "Custom exceptions that yield predictable response codes: hmm sure... Semantic language to wrap all that: now you're talking!"
    ),
    new ProjectMetadata(
      new Date(2011, 9),
      'Griddler',
      'griddler',
      'Fun grid puzzle, later popularised by GCHQ in a Christmas challenge. Perhaps my most cherished side-project :)'
    ),
    new ProjectMetadata(new Date(2021, 5), 'Libra', 'libra', '', false),
    new ProjectMetadata(new Date(2016, 7), 'PSR', 'psr', '', false),
  ];

  getProjects(): Observable<ProjectMetadata[]> {
    return of(this.projects);
  }

  isEnabled(link: string): boolean {
    return this.projects.find((p) => p.link === link)?.enabled || false;
  }
}
